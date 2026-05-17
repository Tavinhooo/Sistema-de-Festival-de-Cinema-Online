using ProjetoES.API.Data;
using ProjetoES.API.Interfaces;
using ProjetoES.API.Models;
using ProjetoES.API.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ProjetoES.API.Services
{
    public class AdministradorService : IAdministradorService
    {
        private readonly IAdministradorRepository _repository;
        private readonly AppDbContext _context;

        public AdministradorService(IAdministradorRepository repository, AppDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        private async Task ValidarAdminAsync(int adminId)
        {
            var admin = await _context.Utilizadores
                .FirstOrDefaultAsync(u => u.Id == adminId && u.Tipo == TipoUtilizador.Administrador);
            if (admin == null)
                throw new UnauthorizedAccessException("Utilizador não é administrador.");
        }

        // ── Filmes ──────────────────────────────────────────
        public async Task<FilmeDTO> GerirFilme(int adminId, FilmeDTO dto)
        {
            await ValidarAdminAsync(adminId);

            if (dto.Id == 0) // criar
            {
                var novoFilme = MapearFilmeDeDTO(dto);
                var criado = await _repository.CriarFilme(novoFilme);
                await SincronizarFilmeComFestivalAsync(criado.Id, dto);
                return MapearFilmeParaDTO(criado);
            }
            else // atualizar
            {
                var filmeExistente = await _repository.ObterFilmePorId(dto.Id)
                    ?? throw new KeyNotFoundException("Filme não encontrado.");
                AtualizarFilmeDeDTO(filmeExistente, dto);
                var atualizado = await _repository.AtualizarFilme(filmeExistente);
                await SincronizarFilmeComFestivalAsync(atualizado.Id, dto);
                return MapearFilmeParaDTO(atualizado);
            }
        }

        public async Task EliminarFilme(int adminId, int filmeId)
        {
            await ValidarAdminAsync(adminId);
            await _repository.EliminarFilme(filmeId);
        }

        // ── Festivais ───────────────────────────────────────
        public async Task<FestivalDTO> GerirFestival(int adminId, FestivalDTO dto)
        {
            await ValidarAdminAsync(adminId);

            if (dto.Id == 0) // criar
            {
                var novoFestival = MapearFestivalDeDTO(dto);
                var criado = await _repository.CriarFestival(novoFestival);
                return MapearFestivalParaDTO(criado);
            }
            else // atualizar
            {
                var festivalExistente = await _repository.ObterFestivalPorId(dto.Id)
                    ?? throw new KeyNotFoundException("Festival não encontrado.");

                if (festivalExistente.DataInicio <= DateOnly.FromDateTime(DateTime.UtcNow))
                    throw new InvalidOperationException("Não é possível editar festival já iniciado.");

                AtualizarFestivalDeDTO(festivalExistente, dto);
                var atualizado = await _repository.AtualizarFestival(festivalExistente);
                return MapearFestivalParaDTO(atualizado);
            }
        }

        public async Task EliminarFestival(int adminId, int festivalId)
        {
            await ValidarAdminAsync(adminId);
            var festival = await _repository.ObterFestivalPorId(festivalId)
                ?? throw new KeyNotFoundException("Festival não encontrado.");

            if (festival.DataInicio <= DateOnly.FromDateTime(DateTime.UtcNow))
                throw new InvalidOperationException("Não é possível cancelar festival já iniciado.");

            await _repository.EliminarFestival(festivalId);
        }

        // ── Sessões ─────────────────────────────────────────
        public async Task<SessaoDTO> GerirSessao(int adminId, SessaoDTO dto)
        {
            await ValidarAdminAsync(adminId);
            var novaSessao = MapearSessaoDeDTO(dto);
            var criada = await _repository.CriarSessao(novaSessao);
            return MapearSessaoParaDTO(criada);
        }

        public async Task AtualizarSessao(int adminId, SessaoDTO dto)
        {
            await ValidarAdminAsync(adminId);
            var sessaoExistente = await _repository.ObterSessaoPorId(dto.Id)
                ?? throw new KeyNotFoundException("Sessão não encontrada.");
            AtualizarSessaoDeDTO(sessaoExistente, dto);
            await _repository.AtualizarSessao(sessaoExistente);
        }

        public async Task CancelarSessao(int adminId, int sessaoId)
        {
            await ValidarAdminAsync(adminId);
            var sessao = await _repository.ObterSessaoPorId(sessaoId)
                ?? throw new KeyNotFoundException("Sessão não encontrada.");

            if (sessao.DataInicio <= DateTime.UtcNow)
                throw new InvalidOperationException("Não é possível cancelar sessão já iniciada.");

            await _repository.EliminarSessao(sessaoId);
        }

        // ── Utilizadores ────────────────────────────────────
        public async Task<IEnumerable<UtilizadorDTO>> ListarUtilizadores(int adminId)
        {
            await ValidarAdminAsync(adminId);
            var utilizadores = await _repository.ObterTodosUtilizadores();
            return utilizadores.Select(MapearUtilizadorParaDTO);
        }

        public async Task<UtilizadorDTO> AlterarTipoUtilizador(int adminId, int utilizadorId, TipoUtilizador novoTipo)
        {
            await ValidarAdminAsync(adminId);
            var utilizador = await _repository.ObterUtilizadorPorId(utilizadorId)
                ?? throw new KeyNotFoundException("Utilizador não encontrado.");
            utilizador.Tipo = novoTipo;
            var atualizado = await _repository.AtualizarUtilizador(utilizador);
            return MapearUtilizadorParaDTO(atualizado);
        }

        public async Task EliminarUtilizador(int adminId, int utilizadorId)
        {
            await ValidarAdminAsync(adminId);
            await _repository.EliminarUtilizador(utilizadorId);
        }

        // ── Avaliações ──────────────────────────────────────
        public async Task<IEnumerable<AvaliacaoDTO>> ListarAvaliacoes(int adminId)
        {
            await ValidarAdminAsync(adminId);
            var avaliacoes = await _repository.ObterTodasAvaliacoes();
            return avaliacoes.Select(MapearAvaliacaoParaDTO);
        }

        public async Task<AvaliacaoDTO> AprovarAvaliacao(int adminId, int avaliacaoId)
        {
            await ValidarAdminAsync(adminId);
            var avaliacao = await _repository.ObterAvaliacaoPorId(avaliacaoId)
                ?? throw new KeyNotFoundException("Avaliação não encontrada.");
            var aprovada = await _repository.AprovarAvaliacao(avaliacao);
            return MapearAvaliacaoParaDTO(aprovada);
        }

        public async Task EliminarAvaliacao(int adminId, int avaliacaoId)
        {
            await ValidarAdminAsync(adminId);
            await _repository.EliminarAvaliacao(avaliacaoId);
        }

        // ── Histórico ───────────────────────────────────────
        public async Task<IEnumerable<PedidoDTO>> ConsultarHistoricoGeral(int adminId, DateTime? de, DateTime? ate)
        {
            await ValidarAdminAsync(adminId);
            var pedidos = await _repository.ConsultarHistoricoGeral(de, ate);
            return pedidos.Select(MapearPedidoParaDTO);
        }

        public async Task<IEnumerable<PedidoDTO>> ConsultarHistoricoPorUtilizador(int adminId, int utilizadorId, DateTime? de, DateTime? ate)
        {
            await ValidarAdminAsync(adminId);
            var pedidos = await _repository.ConsultarHistoricoPorUtilizador(utilizadorId, de, ate);
            return pedidos.Select(MapearPedidoParaDTO);
        }

        // ── Mappers ─────────────────────────────────────────
        private Filme MapearFilmeDeDTO(FilmeDTO dto) => new()
        {
            Titulo = dto.Titulo,
            Sinopse = dto.Sinopse,
            DuracaoMinutos = dto.DuracaoMinutos,
            PosterUrl = dto.PosterUrl
        };

        private void AtualizarFilmeDeDTO(Filme filme, FilmeDTO dto)
        {
            filme.Titulo = dto.Titulo;
            filme.Sinopse = dto.Sinopse;
            filme.DuracaoMinutos = dto.DuracaoMinutos;
            filme.PosterUrl = dto.PosterUrl;
        }

        private FilmeDTO MapearFilmeParaDTO(Filme f)
        {
            var ligacaoFestival = _context.FestivalFilmes
                .AsNoTracking()
                .FirstOrDefault(ff => ff.FilmeId == f.Id);

            return new FilmeDTO
            {
                Id = f.Id,
                Titulo = f.Titulo,
                Sinopse = f.Sinopse,
                DuracaoMinutos = f.DuracaoMinutos,
                PrecoBilhete = ligacaoFestival?.PrecoBilhete ?? 0m,
                PosterUrl = f.PosterUrl,
                FestivalId = ligacaoFestival?.FestivalId ?? 0,
                MediaAvaliacao = f.MediaAvaliacao
            };
        }

        private Festival MapearFestivalDeDTO(FestivalDTO dto) => new()
        {
            Nome = dto.Nome,
            Descricao = dto.Descricao,
            DataInicio = dto.DataInicio,
            DataFim = dto.DataFim
        };

        private void AtualizarFestivalDeDTO(Festival festival, FestivalDTO dto)
        {
            festival.Nome = dto.Nome;
            festival.Descricao = dto.Descricao;
            festival.DataInicio = dto.DataInicio;
            festival.DataFim = dto.DataFim;
        }

        private async Task SincronizarFilmeComFestivalAsync(int filmeId, FilmeDTO dto)
        {
            if (dto.FestivalId <= 0)
            {
                return;
            }

            var festival = await _context.Festivais.FirstOrDefaultAsync(f => f.Id == dto.FestivalId);
            if (festival == null)
            {
                throw new KeyNotFoundException("Festival não encontrado.");
            }

            if (dto.PrecoBilhete <= 0)
            {
                throw new ArgumentException("O preço do bilhete é obrigatório.");
            }

            var ligacao = await _context.FestivalFilmes.FirstOrDefaultAsync(ff => ff.FestivalId == dto.FestivalId && ff.FilmeId == filmeId);
            if (ligacao == null)
            {
                _context.FestivalFilmes.Add(new FestivalFilme
                {
                    FestivalId = dto.FestivalId,
                    FilmeId = filmeId,
                    PrecoBilhete = dto.PrecoBilhete
                });
            }
            else
            {
                ligacao.PrecoBilhete = dto.PrecoBilhete;
            }

            await _context.SaveChangesAsync();
        }

        private FestivalDTO MapearFestivalParaDTO(Festival f) => new()
        {
            Id = f.Id,
            Nome = f.Nome,
            Descricao = f.Descricao,
            DataInicio = f.DataInicio,
            DataFim = f.DataFim
        };

        private Sessao MapearSessaoDeDTO(SessaoDTO dto) => new()
        {
            FilmeId = dto.FilmeId,
            FestivalId = dto.FestivalId ?? 0,
            DataInicio = dto.DataHora,
            Sala = dto.Sala.ToString()
        };

        private void AtualizarSessaoDeDTO(Sessao sessao, SessaoDTO dto)
        {
            sessao.FilmeId = dto.FilmeId;
            sessao.DataInicio = dto.DataHora;
            sessao.Sala = dto.Sala.ToString();
        }

        private SessaoDTO MapearSessaoParaDTO(Sessao s) => new()
        {
            Id = s.Id,
            FilmeId = s.FilmeId,
            DataHora = s.DataInicio,
            Sala = int.TryParse(s.Sala, out var sala) ? sala : 0,
            FestivalId = s.FestivalId
        };

        private UtilizadorDTO MapearUtilizadorParaDTO(Utilizador u) => new()
        {
            Id = u.Id,
            PrimeiroNome = u.PrimeiroNome,
            UltimoNome = u.UltimoNome,
            Email = u.Email,
            Tipo = u.Tipo,
            IsLogged = u.IsLogged,
            DataPrimeiraCompra = u.DataPrimeiraCompra
        };

        private AvaliacaoDTO MapearAvaliacaoParaDTO(Avaliacao a) => new()
        {
            Id = a.Id,
            ClienteId = a.ClienteId,
            FilmeId = a.FilmeId,
            Classificacao = a.Classificacao,
            Comentario = a.Comentario,
            DataAvaliacao = a.DataAvaliacao,
            IsReportado = a.IsReportado
        };

        private PedidoDTO MapearPedidoParaDTO(Pedido p) => new()
        {
            Id = p.Id,
            UtilizadorId = p.UtilizadorId,
            UtilizadorEmail = p.Utilizador?.Email ?? string.Empty,
            DataPedido = p.DataPedido,
            SessaoId = p.SessaoId,
            Quantidade = p.Quantidade,
            Estado = p.Estado.ToString(),
            PrecoTotal = p.PrecoTotal
        };
    }
}