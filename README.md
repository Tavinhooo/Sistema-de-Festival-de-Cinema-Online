# Sistema-de-Festival-de-Cinema-Online
Octávio Abreu (2151223),
Leandro Rodrigues (2104123),
Manuel Gama (2106723),
André Pereira (2052923)

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
│   │   ├── _ViewImports.cshtml
│   │   ├── _ViewStart.cshtml
│   │   ├── AdicionarFestival.cshtml
│   │   ├── AdicionarFilme.cshtml
│   │   ├── AdicionarFilmeAFestival.cshtml
│   │   ├── Admin.cshtml
│   │   ├── Admin.cshtml.cs
│   │   ├── AdminPanel.cshtml
│   │   ├── Acessos/
│   │   │   └── AcessosController.cs
│   │   ├── Admin/
│   │   │   └── AdministradorController.cs
│   │   ├── Auth/
│   │   │   └── AuthController.cs
│   │   ├── Avaliacoes/
│   │   │   └── AvaliacoesController.cs
│   │   ├── Catalogos/
│   │   │   ├── FilmesController.cs
│   │   │   └── FestivaisController.cs
│   │   ├── Compras/
│   │   │   ├── CarrinhosController.cs
│   │   │   └── CheckoutController.cs
│   │   ├── Listas/
│   │   │   └── ListaPessoalController.cs
│   │   ├── Sessoes/
│   │   │   └── SessoesController.cs
│   │   ├── Utilizadores/
│   │   │   ├── ClienteController.cs
│   │   │   ├── MembroController.cs
│   │   │   └── VisitantesController.cs
│   │   ├── PremiosController.cs
│   │   └── RecomendacoesController.cs
│   │   ├── Index.cshtml
│   │   ├── Index.cshtml.cs
│   │   ├── Login.cshtml
│   │   ├── Login.cshtml.cs
│   │   ├── Perfil.cshtml
│   │   ├── Perfil.cshtml.cs
│   │   ├── Acessos/
│   │   │   └── AcessoResponseDTO.cs
│   │   ├── Admin/
│   │   │   └── AdministradorDTOS.cs
│   │   ├── Auth/
│   │   │   ├── AuthLoginDTO.cs
│   │   │   ├── AuthRegisterDTO.cs
│   │   │   └── AuthResponseDTO.cs
│   │   ├── Avaliacoes/
│   │   │   ├── AvaliacaoResponseDTO.cs
│   │   │   ├── CriarAvaliacaoDTO.cs
│   │   │   └── ReportarAvaliacaoDTO.cs
│   │   ├── Catalogos/
│   │   │   ├── AssociarFilmeRequestDTO.cs
│   │   │   ├── CreateFilmeDTO.cs
│   │   │   ├── FestivalFiltroDTO.cs
│   │   │   ├── FestivalRequestDTO.cs
│   │   │   ├── FestivalResponseDTO.cs
│   │   │   ├── FilmeFestivalDTO.cs
│   │   │   ├── FilmeResponseDTO.cs
│   │   │   ├── PrecoResponseDTO.cs
│   │   │   ├── UpdateFilmeDTO.cs
│   │   │   └── VincularFilmeFestivalDTO.cs
│   │   ├── Compras/
│   │   │   ├── AtualizarItemDTO.cs
│   │   │   ├── AtualizarMetodoPagamento.cs
│   │   │   ├── CarrinhoRequestDTO.cs
│   │   │   ├── CarrinhoResponseDTO.cs
│   │   │   ├── CheckoutDTO.cs
│   │   │   ├── HistoricoComprasDTO.cs
│   │   │   ├── ItemCarrinhoRequestDTO.cs
│   │   │   ├── ItemCarrinhoResponseDTO.cs
│   │   │   └── StripeCheckoutSessionDTO.cs
│   │   ├── Sessoes/
│   │   │   ├── SessaoRequestDTO.cs
│   │   │   └── SessaoResponseDTO.cs
│   │   └── Utilizadores/
│   │       ├── AtualizarMoradaDTO.cs
│   │       ├── AtualizarPerfilDTO.cs
│   │       ├── ClienteResponseDTO.cs
│   │       ├── MembroPerfilDTO.cs
│   │       ├── MoradaDTO.cs
│   │       └── VisitanteSessionDTO.cs
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
│   ├── cinema_festival_backup.sql
│   ├── Controllers/
│   │   ├── Acessos/
│   │   │   └── AcessosController.cs
│   │   ├── Admin/
│   │   │   └── AdministradorController.cs
│   │   ├── Auth/
│   │   │   └── AuthController.cs
│   │   ├── Avaliacoes/
│   │   │   └── AvaliacoesController.cs
│   │   ├── Catalogos/
│   │   │   ├── FestivaisController.cs
│   │   │   └── FilmesController.cs
│   │   ├── Compras/
│   │   │   ├── CarrinhosController.cs
│   │   │   └── CheckoutController.cs
│   │   ├── Listas/
│   │   │   └── ListaPessoalController.cs
│   │   ├── Sessoes/
│   │   │   └── SessoesController.cs
│   │   ├── Utilizadores/
│   │   │   ├── ClienteController.cs
│   │   │   ├── MembroController.cs
│   │   │   └── VisitantesController.cs
│   │   ├── PremiosController.cs
│   │   └── RecomendacoesController.cs
│   ├── Data/
│   │   ├── AppDbContext.cs
│   │   ├── AppDbContextFactory.cs
│   │   └── Migrations/
│   ├── DTOs/
│   │   ├── Acessos/
│   │   │   └── AcessoResponseDTO.cs
│   │   ├── Admin/
│   │   │   └── AdministradorDTOS.cs
│   │   ├── Auth/
│   │   │   ├── AuthLoginDTO.cs
│   │   │   ├── AuthRegisterDTO.cs
│   │   │   └── AuthResponseDTO.cs
│   │   ├── Avaliacoes/
│   │   │   ├── AvaliacaoResponseDTO.cs
│   │   │   ├── CriarAvaliacaoDTO.cs
│   │   │   └── ReportarAvaliacaoDTO.cs
│   │   ├── Catalogos/
│   │   │   ├── AssociarFilmeRequestDTO.cs
│   │   │   ├── CreateFilmeDTO.cs
│   │   │   ├── FestivalFiltroDTO.cs
│   │   │   ├── FestivalRequestDTO.cs
│   │   │   ├── FestivalResponseDTO.cs
│   │   │   ├── FilmeFestivalDTO.cs
│   │   │   ├── FilmeResponseDTO.cs
│   │   │   ├── PrecoResponseDTO.cs
│   │   │   ├── UpdateFilmeDTO.cs
│   │   │   └── VincularFilmeFestivalDTO.cs
│   │   ├── Compras/
│   │   │   ├── AtualizarItemDTO.cs
│   │   │   ├── AtualizarMetodoPagamento.cs
│   │   │   ├── CarrinhoRequestDTO.cs
│   │   │   ├── CarrinhoResponseDTO.cs
│   │   │   ├── CheckoutDTO.cs
│   │   │   ├── HistoricoComprasDTO.cs
│   │   │   ├── ItemCarrinhoRequestDTO.cs
│   │   │   ├── ItemCarrinhoResponseDTO.cs
│   │   │   └── StripeCheckoutSessionDTO.cs
│   │   ├── CriarPremioDTO.cs
│   │   ├── Sessoes/
│   │   │   ├── SessaoRequestDTO.cs
│   │   │   └── SessaoResponseDTO.cs
│   │   └── Utilizadores/
│   │       ├── AtualizarMoradaDTO.cs
│   │       ├── AtualizarPerfilDTO.cs
│   │       ├── ClienteResponseDTO.cs
│   │       ├── MembroPerfilDTO.cs
│   │       ├── MoradaDTO.cs
│   │       └── VisitanteSessionDTO.cs
│   ├── Factories/
│   │   ├── Acessos/
│   │   │   ├── AcessoFactory.cs
│   │   │   ├── AluguerDigitalFactory.cs
│   │   │   ├── BilheteAcessoFactory.cs
│   │   │   ├── PasseCompletoAcessoFactory.cs
│   │   │   └── PasseDiarioAcessoFactory.cs
│   │   └── Listas/
│   │       └── ListaPessoalFactory.cs
│   ├── Interfaces/
│   │   ├── Admin/
│   │   │   ├── IAdministradorRepository.cs
│   │   │   └── IAdministradorService.cs
│   │   ├── Avaliacoes/
│   │   │   ├── IAvaliacaoObservable.cs
│   │   │   └── IAvaliacaoObserver.cs
│   │   ├── Catalogos/
│   │   │   ├── IFilmeService.cs
│   │   │   ├── ITmdbApiClient.cs
│   │   │   └── ITmdbService.cs
│   │   └── Listas/
│   │       ├── IListaPessoalRepository.cs
│   │       ├── IListaPessoalService.cs
│   │       └── IListaPessoalStrategy.cs
│   ├── Migrations/
│   │   ├── 20260515151246_InitialCreate.cs
│   │   ├── 20260515151246_InitialCreate.Designer.cs
│   │   ├── 20260515162413_AddLocalToFestival.cs
│   │   ├── 20260515162413_AddLocalToFestival.Designer.cs
│   │   ├── 20260516140526_TornarSessaoIdNullable.cs
│   │   └── ...
│   ├── Models/
│   │   ├── Acessos/
│   │   │   ├── Acesso.cs
│   │   │   └── LogAlteracaoAcesso.cs
│   │   ├── Avaliacoes/
│   │   │   └── Avaliacao.cs
│   │   ├── Catalogos/
│   │   │   ├── EstadoFestival.cs
│   │   │   ├── Festival.cs
│   │   │   ├── FestivalFilme.cs
│   │   │   ├── Filme.cs
│   │   │   └── TmdbMovie.cs
│   │   ├── Compras/
│   │   │   ├── Carrinho.cs
│   │   │   ├── Compra.cs
│   │   │   ├── ItemPedido.cs
│   │   │   └── Pedido.cs
│   │   ├── External/
│   │   │   └── TmdbApiDtos.cs
│   │   ├── Listas/
│   │   │   ├── ListaPessoal.cs
│   │   │   └── TipoLista.cs
│   │   ├── Premio.cs
│   │   ├── Sessoes/
│   │   │   └── Sessao.cs
│   │   ├── Utilizadores/
│   │   │   ├── Administrador.cs
│   │   │   ├── Membro.cs
│   │   │   ├── Morada.cs
│   │   │   ├── PasswordResetToken.cs
│   │   │   ├── TipoUtilizadores.cs
│   │   │   ├── Utilizador.cs
│   │   │   └── Visitante.cs
│   │   └── VotoPremio.cs
│   ├── Pricing/
│   ├── Properties/
│   ├── Repositories/
│   ├── scripts/
│   └── Services/
└── Projeto.pdf
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
