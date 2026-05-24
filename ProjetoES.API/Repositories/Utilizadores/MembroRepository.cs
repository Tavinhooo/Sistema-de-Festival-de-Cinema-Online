using Microsoft.EntityFrameworkCore;
using ProjetoES.API.Data;
using ProjetoES.API.Models;

namespace ProjetoES.API.Repositories;
/// <summary>
/// Repositório de membros, responsável por gerenciar as operações relacionadas aos membros,
/// incluindo a obtenção de informações do membro e a atualização dos dados do membro.
/// </summary>
public class MembroRepository
{
    private readonly AppDbContext _context;

    public MembroRepository(AppDbContext context)
    {
        _context = context;
    }

    public Utilizador? ObterPorId(int id)
    {
        return _context.Set<Utilizador>()
            .FirstOrDefault(u => u.Id == id);
    }

    public void AtualizarMembro(Utilizador utilizador)
    {
        _context.Set<Utilizador>().Update(utilizador);
        _context.SaveChanges();
    }
}