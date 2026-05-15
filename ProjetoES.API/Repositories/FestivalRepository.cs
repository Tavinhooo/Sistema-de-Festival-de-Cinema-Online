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
        var now = DateTime.UtcNow;
        return _context.Festivais
            .Where(f => f.DataInicio <= now && f.DataFim >= now)
            .OrderBy(f => f.DataInicio)
            .ToList();
    }

    public List<Festival> ObterFestivaisFuturos()
    {
        var now = DateTime.UtcNow;
        return _context.Festivais
            .Where(f => f.DataInicio > now)
            .OrderBy(f => f.DataInicio)
            .ToList();
    }

    public List<Festival> ObterFestivaisDisponiveisParaFilmes()
    {
        var now = DateTime.UtcNow;
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

    public List<Festival> FiltrarFestivais(string? nome = null, DateTime? dataInicio = null, DateTime? dataFim = null, string? local = null)
    {
        var query = _context.Festivais.AsQueryable();

        if (!string.IsNullOrWhiteSpace(nome))
        {
            query = query.Where(f => f.Nome.ToLower().Contains(nome.ToLower()));
        }

        if (dataInicio.HasValue)
        {
            query = query.Where(f => f.DataInicio >= dataInicio.Value);
        }

        if (dataFim.HasValue)
        {
            query = query.Where(f => f.DataFim <= dataFim.Value);
        }

        if (!string.IsNullOrWhiteSpace(local))
        {
            query = query.Where(f => f.Local.ToLower().Contains(local.ToLower()));
        }

        return query.OrderBy(f => f.DataInicio).ToList();
    }

    public List<Festival> ObterFestivaisADecorrer()
    {
        var now = DateTime.UtcNow;
        return _context.Festivais
            .Where(f => f.DataInicio <= now && f.DataFim >= now)
            .OrderBy(f => f.DataInicio)
            .ToList();
    }

    public List<Festival> ObterFestivaisFuturos()
    {
        var now = DateTime.UtcNow;
        return _context.Festivais
            .Where(f => f.DataInicio > now)
            .OrderBy(f => f.DataInicio)
            .ToList();
    }
}
