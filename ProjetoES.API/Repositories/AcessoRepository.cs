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
}