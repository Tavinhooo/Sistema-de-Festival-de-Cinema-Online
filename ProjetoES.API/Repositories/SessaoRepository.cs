using Microsoft.EntityFrameworkCore;
using ProjetoES.API.Data;
using ProjetoES.API.Models;

namespace ProjetoES.API.Repositories;

public class SessaoRepository
{
    private readonly AppDbContext _context;

    public SessaoRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<Sessao> ObterTodasSessoes()
    {
        return _context.Sessoes
            .Include(s => s.Festival)
            .Include(s => s.Filme)
            .ToList();
    }

    public Sessao? ObterSessaoPorId(int id)
    {
        return _context.Sessoes
            .Include(s => s.Festival)
            .Include(s => s.Filme)
            .FirstOrDefault(s => s.Id == id);
    }

    public List<Sessao> ObterSessoesPorFestival(int festivalId)
    {
        return _context.Sessoes
            .Include(s => s.Festival)
            .Include(s => s.Filme)
            .Where(s => s.FestivalId == festivalId)
            .ToList();
    }

    public List<Sessao> ObterSessoesPorFilme(int filmeId)
    {
        return _context.Sessoes
            .Include(s => s.Festival)
            .Include(s => s.Filme)
            .Where(s => s.FilmeId == filmeId)
            .ToList();
    }

    public void AdicionarSessao(Sessao sessao)
    {
        _context.Sessoes.Add(sessao);
        _context.SaveChanges();
    }

    public void AtualizarSessao(Sessao sessao)
    {
        _context.Sessoes.Update(sessao);
        _context.SaveChanges();
    }

    public void EliminarSessao(int id)
    {
        var sessao = _context.Sessoes.Find(id);
        if (sessao != null)
        {
            _context.Sessoes.Remove(sessao);
            _context.SaveChanges();
        }
    }
}