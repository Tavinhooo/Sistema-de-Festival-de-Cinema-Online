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

$env:PGPASSWORD = $pgPassword

function Invoke-Psql {
    param(
        [Parameter(Mandatory = $true)]
        [string]$Database,
        [Parameter(Mandatory = $true)]
        [string[]]$ExtraArguments
    )

    & psql -h $pgHost -p $pgPort -U $pgUsername -d $Database @ExtraArguments

    if ($LASTEXITCODE -ne 0) {
        throw "psql command failed while targeting database '$Database'."
    }
}

function Test-DatabaseExists {
    $result = & psql -h $pgHost -p $pgPort -U $pgUsername -d postgres -tAc "SELECT 1 FROM pg_database WHERE datname = '$pgDatabase';"

    if ($LASTEXITCODE -ne 0) {
        throw "Could not check whether database '$pgDatabase' exists."
    }

    return $result.Trim() -eq '1'
}

function Test-DatabaseHasUserTables {
    $tableCount = & psql -h $pgHost -p $pgPort -U $pgUsername -d $pgDatabase -tAc "SELECT COUNT(*) FROM pg_catalog.pg_tables WHERE schemaname NOT IN ('pg_catalog', 'information_schema');"

    if ($LASTEXITCODE -ne 0) {
        throw "Could not inspect database '$pgDatabase'."
    }

    return ([int]$tableCount.Trim()) -gt 0
}

$databaseExists = Test-DatabaseExists

if (-not $databaseExists) {
    Write-Host "[bootstrap-db] Database '$pgDatabase' does not exist yet. Creating it..."
    Invoke-Psql -Database 'postgres' -ExtraArguments @('-v', 'ON_ERROR_STOP=1', '-c', ('CREATE DATABASE "{0}";' -f $pgDatabase))
    $databaseExists = $true
}

$shouldRecreateDb = $RecreateDb -or (Test-DatabaseHasUserTables)

if ($shouldRecreateDb) {
    Write-Host "[bootstrap-db] Recreating database '$pgDatabase' before restore..."

    $terminateSql = "SELECT pg_terminate_backend(pid) FROM pg_stat_activity WHERE datname = '{0}' AND pid <> pg_backend_pid();" -f $pgDatabase
    Invoke-Psql -Database 'postgres' -ExtraArguments @('-v', 'ON_ERROR_STOP=1', '-c', $terminateSql)

    $dropSql = 'DROP DATABASE IF EXISTS "{0}";' -f $pgDatabase
    $createSql = 'CREATE DATABASE "{0}";' -f $pgDatabase
    Invoke-Psql -Database 'postgres' -ExtraArguments @('-v', 'ON_ERROR_STOP=1', '-c', $dropSql)
    Invoke-Psql -Database 'postgres' -ExtraArguments @('-v', 'ON_ERROR_STOP=1', '-c', $createSql)

    Write-Host "[bootstrap-db] Database recreated. Proceeding to restore."
}

Write-Host "Restoring PostgreSQL dump into '$pgDatabase'..."
Invoke-Psql -Database $pgDatabase -ExtraArguments @('-v', 'ON_ERROR_STOP=1', '-f', $dumpPath)

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