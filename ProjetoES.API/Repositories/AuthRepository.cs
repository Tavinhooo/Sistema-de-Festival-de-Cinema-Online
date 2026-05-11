using Microsoft.EntityFrameworkCore;
using ProjetoES.API.Data;
using ProjetoES.API.Models;

namespace ProjetoES.API.Repositories;

public class AuthRepository
{
    private readonly AppDbContext _context;

    public AuthRepository(AppDbContext context)
    {
        _context = context;
    }

    public Membro? ObterPorEmail(string email)
    {
        return _context.Set<Membro>().FirstOrDefault(u => u.Email == email);
    }

    public void CriarMembro(Membro membro)
    {
        _context.Add(membro);
        _context.SaveChanges();
    }
}
