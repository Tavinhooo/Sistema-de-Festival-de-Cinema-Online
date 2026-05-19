using Microsoft.EntityFrameworkCore;
using ProjetoES.API.Data;
using ProjetoES.API.Models;

namespace ProjetoES.API.Repositories;

public class FestivalRepository
{
    private readonly AppDbContext _context;

    public FestivalRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<Festival> ObterTodosFestivais()
    {
        return _context.Festivais.ToList();
    }

    public List<Festival> ObterFestivaisADecorrer()
    {
        var now = DateOnly.FromDateTime(DateTime.UtcNow);
        return _context.Festivais
            .Where(f => f.DataInicio <= now && f.DataFim >= now)
            .OrderBy(f => f.DataInicio)
            .ToList();
    }

    public List<Festival> ObterFestivaisFuturos()
    {
        var now = DateOnly.FromDateTime(DateTime.UtcNow);
        return _context.Festivais
            .Where(f => f.DataInicio > now)
            .OrderBy(f => f.DataInicio)
            .ToList();
    }

    public List<Festival> ObterFestivaisDisponiveisParaFilmes()
    {
        var now = DateOnly.FromDateTime(DateTime.UtcNow);
        return _context.Festivais
            .Where(f => f.DataInicio >= now)
            .OrderBy(f => f.DataInicio)
            .ToList();
    }

    public Festival? ObterFestivalPorId(int id)
    {
        return _context.Festivais.Find(id);
    }

    public void AdicionarFestival(Festival festival)
    {
        _context.Festivais.Add(festival);
        _context.SaveChanges();
    }

    public void UpdateFestival(Festival festival)
    {
        _context.Festivais.Update(festival);
        _context.SaveChanges();
    }

    public void DeleteFestival(int id)
    {
        var festival = _context.Festivais.Find(id);
        if (festival != null)
        {
            _context.Festivais.Remove(festival);
            _context.SaveChanges();
        }
    }

    public List<Festival> FiltrarFestivais(string? nome = null, string? descricao = null, DateOnly? dataInicio = null, DateOnly? dataFim = null, string? local = null)
    {
        var query = _context.Festivais.AsQueryable();

        if (!string.IsNullOrWhiteSpace(nome))
            query = query.Where(f => f.Nome.ToLower().Contains(nome.ToLower()));
            
        if (!string.IsNullOrWhiteSpace(descricao))
            query = query.Where(f => f.Descricao.ToLower().Contains(descricao.ToLower()));

        if (dataInicio.HasValue)
            query = query.Where(f => f.DataInicio >= dataInicio.Value);

        if (dataFim.HasValue)
            query = query.Where(f => f.DataFim <= dataFim.Value);

        if (!string.IsNullOrWhiteSpace(local))
            query = query.Where(f => f.Local.ToLower().Contains(local.ToLower()));

        return query.OrderBy(f => f.DataInicio).ToList();
    }

    public void AssociarFilmeAoFestival(int festivalId, Filme filme)
    {
        var festival = _context.Festivais
            .Include(f => f.Filmes) 
            .FirstOrDefault(f => f.Id == festivalId);

        if (festival == null)
            throw new ArgumentException("Festival não encontrado.");

        // Verifica se a relação já existe na base de dados para não duplicar
        if (!festival.Filmes.Any(filmeNoFestival => filmeNoFestival.Id == filme.Id))
        {
            festival.Filmes.Add(filme);
            _context.SaveChanges();
        }
    }
}