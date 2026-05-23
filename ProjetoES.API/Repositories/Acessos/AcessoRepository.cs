using Microsoft.EntityFrameworkCore;
using ProjetoES.API.Data;
using ProjetoES.API.Models;

namespace ProjetoES.API.Repositories;

public class AcessoRepository
{
    private readonly AppDbContext _context;

    public AcessoRepository(AppDbContext context)
    {
        _context = context;
    }

    public void CriarAcessos(IEnumerable<Acesso> acessos)
    {
        _context.Acessos.AddRange(acessos);
        _context.SaveChanges();
    }
   
    public bool VerificarAcessoFilmeNoFestival(int clienteId, int filmeId, int festivalId)
    {
        bool temBilheteFilme = _context.Acessos.Any(a =>
            a.ClienteId == clienteId &&
            a.FilmeId == filmeId &&
            a.Estado == EstadoAcesso.Ativo &&
            (a.TipoAcesso == "Bilhete de Sessão" || a.TipoAcesso == "Aluguer Digital"));

        if (temBilheteFilme) return true;

        bool filmePertenceAoFestival = _context.FestivalFilmes
            .Any(ff => ff.FestivalId == festivalId && ff.FilmeId == filmeId);

        if (filmePertenceAoFestival)
        {
            var filmesDoFestival = _context.FestivalFilmes
                .Where(ff => ff.FestivalId == festivalId)
                .Select(ff => ff.FilmeId)
                .ToList();

            bool temPasseFestival = _context.Acessos.Any(a =>
                a.ClienteId == clienteId &&
                a.Estado == EstadoAcesso.Ativo &&
                (a.TipoAcesso == "Passe Completo" || a.TipoAcesso == "Passe Diário") &&
                filmesDoFestival.Contains(a.FilmeId));

            if (temPasseFestival) return true;
        }

        return false;
    }
    
    public List<Filme> ObterFilmesComAcesso(int clienteId)
    {
        return _context.Acessos
            .Where(a => a.ClienteId == clienteId && a.Estado == EstadoAcesso.Ativo)
            .Include(a => a.Filme)
            .Select(a => a.Filme)
            .ToList();
    }
}