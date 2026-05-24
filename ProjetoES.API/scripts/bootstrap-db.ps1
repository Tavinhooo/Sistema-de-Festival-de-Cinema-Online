param(
    [switch]$RunApp,
    [switch]$RecreateDb
)

$ErrorActionPreference = 'Stop'

$scriptRoot = Split-Path -Parent $MyInvocation.MyCommand.Path
$projectRoot = Split-Path -Parent $scriptRoot
$settingsPath = Join-Path $projectRoot 'appsettings.json'
$dumpPath = Join-Path $projectRoot 'cinema_festival_backup.sql'

if (-not (Test-Path $settingsPath)) {
    throw "Could not find appsettings.json at '$settingsPath'."
}

if (-not (Test-Path $dumpPath)) {
    throw "Could not find the database dump at '$dumpPath'."
}

$settings = Get-Content $settingsPath -Raw | ConvertFrom-Json
$connectionString = $settings.ConnectionStrings.DefaultConnection

if ([string]::IsNullOrWhiteSpace($connectionString)) {
    throw 'DefaultConnection is missing from appsettings.json.'
}


# Manually parse the connection string into a dictionary (case-insensitive)
$kv = @{}
foreach ($segment in $connectionString.Split(';')) {
    if ([string]::IsNullOrWhiteSpace($segment)) { continue }
    $parts = $segment.Split('=', 2)
    if ($parts.Length -lt 2) { continue }
    $key = $parts[0].Trim().ToLowerInvariant()
    $value = $parts[1].Trim()
    $kv[$key] = $value
}

function Get-ConnectionValue {
    param(
        [Parameter(Mandatory = $true)]
        [string]$Key
    )

    $k = $Key.ToLowerInvariant()
    $aliases = @($k)

    switch ($k) {
        'username' { $aliases += @('user id','userid','user','uid') }
        'password' { $aliases += @('pwd') }
        'database' { $aliases += @('initial catalog') }
        default { }
    }

    foreach ($a in $aliases) {
        if ($kv.ContainsKey($a)) { return $kv[$a] }
    }

    return $null
}


$pgHost = Get-ConnectionValue 'Host'
$pgPort = Get-ConnectionValue 'Port'
$pgDatabase = Get-ConnectionValue 'Database'
$pgUsername = Get-ConnectionValue 'Username'
$pgPassword = Get-ConnectionValue 'Password'

Write-Host "[bootstrap-db] ConnectionString: $connectionString"
Write-Host "[bootstrap-db] Parsed connection-string keys:";
foreach ($entry in $kv.GetEnumerator()) {
    Write-Host ("[bootstrap-db] {0} = {1}" -f $entry.Key, $entry.Value)
}

if ([string]::IsNullOrWhiteSpace($pgHost) -or
    [string]::IsNullOrWhiteSpace($pgPort) -or
    [string]::IsNullOrWhiteSpace($pgDatabase) -or
    [string]::IsNullOrWhiteSpace($pgUsername)) {
    throw 'DefaultConnection must include Host, Port, Database, and Username.'
}


if ($RecreateDb) {
    Write-Host "[bootstrap-db] RecreateDb requested: terminating connections and recreating database '$pgDatabase'..."
    $env:PGPASSWORD = $pgPassword

    # Terminate connections to the target database
    $terminateSql = "SELECT pg_terminate_backend(pid) FROM pg_stat_activity WHERE datname = '{0}' AND pid <> pg_backend_pid();" -f $pgDatabase
    & psql -h $pgHost -p $pgPort -U $pgUsername -d postgres -c $terminateSql

    # Drop and recreate database (DROP cannot run inside a transaction block)
    $dropSql = 'DROP DATABASE IF EXISTS "{0}";' -f $pgDatabase
    $createSql = 'CREATE DATABASE "{0}";' -f $pgDatabase
    & psql -h $pgHost -p $pgPort -U $pgUsername -d postgres -c $dropSql
    & psql -h $pgHost -p $pgPort -U $pgUsername -d postgres -c $createSql

    if ($LASTEXITCODE -ne 0) {
        throw 'Failed to drop/create database.'
    }

    Write-Host "[bootstrap-db] Database recreated. Proceeding to restore."
} else {
    $env:PGPASSWORD = $pgPassword
}

$psqlArgs = @(
    '-h', $pgHost,
    '-p', $pgPort,
    '-U', $pgUsername,
    '-d', $pgDatabase,
    '-f', $dumpPath
)

Write-Host "Restoring PostgreSQL dump into '$pgDatabase'..."
& psql @psqlArgs

if ($LASTEXITCODE -ne 0) {
    throw 'Database restore failed.'
}

Write-Host 'Database restore completed successfully.'

if ($RunApp) {
    Push-Location $projectRoot
    try {
        dotnet run
    }
    finally {
        Pop-Location
    }
}