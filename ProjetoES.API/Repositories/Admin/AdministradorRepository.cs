using ProjetoES.API.Data;
using ProjetoES.API.Interfaces;
using ProjetoES.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ProjetoES.API.Repositories
{
    public class AdministradorRepository : IAdministradorRepository
    {
        private readonly AppDbContext _context;
        public AdministradorRepository(AppDbContext context) => _context = context;

        public async Task<Filme> CriarFilme(Filme filme)
        {
            await _context.Filmes.AddAsync(filme);
            await _context.SaveChangesAsync();
            return filme;
        }
        public async Task<Filme?> ObterFilmePorId(int id) => await _context.Filmes.FindAsync(id);
        public async Task<IEnumerable<Filme>> ObterTodosFilmes() => await _context.Filmes.ToListAsync();
        public async Task<Filme> AtualizarFilme(Filme filme)
        {
            _context.Filmes.Update(filme);
            await _context.SaveChangesAsync();
            return filme;
        }
        public async Task EliminarFilme(int id)
        {
            var filme = await _context.Filmes.FindAsync(id);
            if (filme != null)
            {
                _context.Filmes.Remove(filme);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Festival> CriarFestival(Festival festival)
        {
            await _context.Festivais.AddAsync(festival);
            await _context.SaveChangesAsync();
            return festival;
        }
        public async Task<Festival?> ObterFestivalPorId(int id) => await _context.Festivais.FindAsync(id);
        public async Task<IEnumerable<Festival>> ObterTodosFestivais() => await _context.Festivais.ToListAsync();
        public async Task<Festival> AtualizarFestival(Festival festival)
        {
            _context.Festivais.Update(festival);
            await _context.SaveChangesAsync();
            return festival;
        }
        public async Task EliminarFestival(int id)
        {
            var festival = await _context.Festivais.FindAsync(id);
            if (festival != null)
            {
                _context.Festivais.Remove(festival);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Sessao> CriarSessao(Sessao sessao)
        {
            await _context.Sessoes.AddAsync(sessao);
            await _context.SaveChangesAsync();
            return sessao;
        }

        public async Task<Sessao?> ObterSessaoPorId(int id) => await _context.Sessoes.FindAsync(id);
        public async Task<IEnumerable<Sessao>> ObterTodasSessoes() => await _context.Sessoes.ToListAsync();
        public async Task<Sessao> AtualizarSessao(Sessao sessao)
        {
            _context.Sessoes.Update(sessao);
            await _context.SaveChangesAsync();
            return sessao;
        }
        public async Task EliminarSessao(int id)
        {
            var sessao = await _context.Sessoes.FindAsync(id);
            if (sessao != null)
            {
                _context.Sessoes.Remove(sessao);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Utilizador>> ObterTodosUtilizadores() => await _context.Utilizadores.ToListAsync();
        public async Task<Utilizador?> ObterUtilizadorPorId(int id) => await _context.Utilizadores.FindAsync(id);
        public async Task<Utilizador> AtualizarUtilizador(Utilizador utilizador)
        {
            _context.Utilizadores.Update(utilizador);
            await _context.SaveChangesAsync();
            return utilizador;
        }
        public async Task EliminarUtilizador(int id)
        {
            var utilizador = await _context.Utilizadores.FindAsync(id);
            if (utilizador != null)
            {
                _context.Utilizadores.Remove(utilizador);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Avaliacao>> ObterTodasAvaliacoes() => await _context.Avaliacoes
            .Include(a => a.Filme)
                .ThenInclude(f => f.Festivais)
            .Include(a => a.Cliente)
            .ToListAsync();
        public async Task<Avaliacao?> ObterAvaliacaoPorId(int id) => await _context.Avaliacoes
            .Include(a => a.Filme)
                .ThenInclude(f => f.Festivais)
            .Include(a => a.Cliente)
            .FirstOrDefaultAsync(a => a.Id == id);
        public async Task<Avaliacao> AprovarAvaliacao(Avaliacao avaliacao)
        {
            avaliacao.IsReportado = true;
            _context.Avaliacoes.Update(avaliacao);
            await _context.SaveChangesAsync();
            return avaliacao;
        }

        public async Task EliminarAvaliacao(int id)
        {
            var avaliacao = await _context.Avaliacoes.FindAsync(id);
            if (avaliacao != null)
            {
                _context.Avaliacoes.Remove(avaliacao);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Pedido>> ConsultarHistoricoGeral(DateTime? de, DateTime? ate)
        {
            var query = _context.Pedidos.AsQueryable();
            if (de.HasValue) query = query.Where(p => p.DataPedido >= de.Value);
            if (ate.HasValue) query = query.Where(p => p.DataPedido <= ate.Value);
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Pedido>> ConsultarHistoricoPorUtilizador(int utilizadorId, DateTime? de, DateTime? ate)
        {
            var query = _context.Pedidos.Where(p => p.UtilizadorId == utilizadorId);
            if (de.HasValue) query = query.Where(p => p.DataPedido >= de.Value);
            if (ate.HasValue) query = query.Where(p => p.DataPedido <= ate.Value);
            return await query.ToListAsync();
        }
    }
    
        
}