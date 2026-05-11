using Microsoft.EntityFrameworkCore;
using ProjetoES.API.Data;
using ProjetoES.API.Interfaces;
using ProjetoES.API.Models;

namespace ProjetoES.API.Repositories;

public class ListaPessoalRepository : IListaPessoalRepository
{
    private readonly AppDbContext _context;
    public ListaPessoalRepository(AppDbContext context) => _context = context;
    public ListaPessoal? ObterPorId(int id) => _context.ListaPessoais
        .Include(l => l.Filmes)
        .FirstOrDefault(l => l.Id == id);
    public IEnumerable<ListaPessoal> ObterPorMembro(int membroId) => _context.ListaPessoais
        .Include(l => l.Filmes)
        .Where(l => l.MembroId == membroId)
        .ToList();
    
    public void Adicionar(ListaPessoal lista) => _context.ListaPessoais.Add(lista);
    public void Atualizar(ListaPessoal lista) => _context.ListaPessoais.Update(lista);
    public void Remover(int id)
    {
        var lista = ObterPorId(id);
        if (lista != null)
        {
            _context.ListaPessoais.Remove(lista);
        }
    }

}
