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
}
