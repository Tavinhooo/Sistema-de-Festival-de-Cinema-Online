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

    public bool TemAcessoAFestival(int clienteId, int festivalId)
    {
        var filmesNoFestival = _context.FestivalFilmes
            .Where(ff => ff.FestivalId == festivalId)
            .Select(ff => ff.FilmeId)
            .ToList();

        return _context.Acessos.Any(a =>
            a.ClienteId == clienteId &&
            filmesNoFestival.Contains(a.FilmeId) &&
            a.Estado == EstadoAcesso.Ativo);
    }

    public List<int> ObterFilmesComAcesso(int clienteId)
    {
        return _context.Acessos
            .Where(a => a.ClienteId == clienteId && a.Estado == EstadoAcesso.Ativo)
            .Select(a => a.FilmeId)
            .ToList();
    }
}