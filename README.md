# Sistema-de-Festival-de-Cinema-Online
Octávio Abreu (2151223),
Leandro Rodrigues (2104123),
Manuel Gama (2106723),
André Pereira (2052923)

💻 Environment Setup
1. Instala o .NET SDK e o PostgreSQL.
2. Usar o script cria a base, restaura os dados.

```powershell
cd ProjetoES.API
.\scripts\bootstrap-db.ps1
psql -h localhost -U postgres -d cinema_festival -f cinema_festival_backup.sql
```

Depois podes arrancar os dois projetos em terminais separados:

```git terminal
cd ProjetoES.API
dotnet run
```

```git terminal
cd ProjetoES
dotnet run
```

Contas de teste:

- Member: `joaoRatao@gmail.com` / `1234!`
- Cliente: `carlaOrnelas@hotmail.com` / `4321!`
- Admin: `admin@festival.com` / `Admin@123!`

Exemplos de testes como utilizador:

- Adicionar um filme ao carrinho e terminar a compra.
- Abrir a lista de festivais e explorar os filmes de cada festival.
- Ver os filmes já comprados na área pessoal.
- Criar e consultar uma lista pessoal de filmes.

Exemplos de testes como administrador:

- Importar filmes para o catálogo.
- Criar um novo festival.
- Associar um filme a um festival.
- Apagar uma avaliação reportada.

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
