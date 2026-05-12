using ProjetoES.API.Data;
using ProjetoES.API.Models;

namespace ProjetoES.API.Repositories;

public class FilmeRepository
{
    private readonly AppDbContext _context;

    public FilmeRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<Filme> ObterTodosFilmes()
    {
        return _context.Filmes.ToList();
    }

    public Filme? ObterFilmePorId(int id)
    {
        return _context.Filmes.Find(id);
    }

    public List<Filme> ObterFilmesPorFestival(int festivalId)
    {
        return _context.Filmes.Where(f => f.Festivais.Any(fest => fest.Id == festivalId)).ToList();
    }

    public void AdicionarFilme(Filme filme)
    {
        _context.Filmes.Add(filme);
        _context.SaveChanges();
    }

    public void AtualizarFilme(Filme filme)
    {
        _context.Filmes.Update(filme);
        _context.SaveChanges();
    }

    public void EliminarFilme(int id)
    {
        var filme = _context.Filmes.Find(id);
        if (filme != null)
        {
            _context.Filmes.Remove(filme);
            _context.SaveChanges();
        }
    }
}
