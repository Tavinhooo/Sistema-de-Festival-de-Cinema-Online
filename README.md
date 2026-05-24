# Sistema-de-Festival-de-Cinema-Online
💻 Environment Setup


```text
Sistema-de-Festival-de-Cinema-Online/
├── README.md
├── Sistema-de-Festival-de-Cinema-Online.sln
├── ProjetoES/
│   ├── ProjetoES.csproj
│   ├── Program.cs
│   ├── appsettings.json
│   ├── appsettings.Development.json
│   ├── Pages/
│   │   ├── Index.cshtml
│   │   ├── Index.cshtml.cs
│   │   ├── Filmes.cshtml
│   │   ├── Filmes.cshtml.cs
│   │   ├── Filme.cshtml
│   │   └── Shared/
│   ├── wwwroot/
│   │   ├── css/
│   │   ├── js/
│   │   └── lib/
│   └── Properties/
│       └── launchSettings.json
├── ProjetoES.API/
│   ├── ProjetoES.API.csproj
│   ├── Program.cs
│   ├── appsettings.json
│   ├── appsettings.Development.json
│   ├── Controllers/
│   │   ├── AuthController.cs
│   │   ├── FilmesController.cs
│   │   ├── FestivaisController.cs
│   │   ├── CarrinhosController.cs
│   │   └── (other controllers...)
│   ├── Data/
│   │   ├── AppDbContext.cs
│   │   ├── AppDbContextFactory.cs
│   │   └── Migrations/
│   ├── DTOs/
│   ├── Models/
│   ├── Repositories/
│   ├── Services/
│   ├── Factories/
│   ├── Pricing/
│   ├── Interfaces/
│   ├── Migrations/
│   ├── Properties/
│   └── wwwroot/
└── README_STRUCTURE.md
```

## Database export and restore

The application already runs `Database.Migrate()` on startup, so a fresh machine only needs the schema plus your data. To ship your current PostgreSQL data with the project, export the database to a dump file and restore it on the next machine.

This repository now includes a local bootstrap script at [ProjetoES.API/scripts/bootstrap-db.ps1](ProjetoES.API/scripts/bootstrap-db.ps1). It reads the connection string from [ProjetoES.API/appsettings.json](ProjetoES.API/appsettings.json) and restores [ProjetoES.API/cinema_festival_backup.sql](ProjetoES.API/cinema_festival_backup.sql) into the configured database.

### Export from your machine

```powershell
pg_dump -h localhost -U postgres -d cinema_festival -F p -f cinema_festival_backup.sql
```

If you prefer a compact binary backup, use custom format instead:

```powershell
pg_dump -h localhost -U postgres -d cinema_festival -F c -f cinema_festival_backup.dump
```

### Restore on another machine

For a plain SQL file:

```powershell
psql -h localhost -U postgres -d cinema_festival -f cinema_festival_backup.sql
```

For a custom-format dump:

```powershell
pg_restore -h localhost -U postgres -d cinema_festival cinema_festival_backup.dump
```

### Run the project with your data

From the `ProjetoES.API` folder, run:

```powershell
.\scripts\bootstrap-db.ps1 -RunApp
```

That restores the dump first and then starts the API. If you only want to restore the database, omit `-RunApp`.

### If you want it automatic

If you want every fresh clone to start with the same records, the usual options are:

1. Keep the dump file in the repository and restore it once during setup.
2. Convert the important rows into EF Core seed data in code.
3. Add a startup routine that imports the dump only when the database is empty.

For your project, option 1 is the fastest way to preserve all current values exactly as they are.
