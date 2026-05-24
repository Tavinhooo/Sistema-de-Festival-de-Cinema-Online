using Microsoft.EntityFrameworkCore;
using ProjetoES.API.Data;
using ProjetoES.API.Models;

namespace ProjetoES.API.Repositories;

public class CarrinhoRepository
{
    private readonly AppDbContext _context;

    public CarrinhoRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<Carrinho> ObterTodosCarrinhos()
    {
        return _context.Carrinhos
            .Include(c => c.Itens)
            .ThenInclude(i => i.Filme)
            .Include(c => c.Itens)
            .ThenInclude(i => i.Festival)
            .ToList();
    }

    public Carrinho? ObterCarrinhoPorId(int id)
    {
        return _context.Carrinhos
            .Include(c => c.Itens)
            .ThenInclude(i => i.Filme)
            .Include(c => c.Itens)
            .ThenInclude(i => i.Festival)
            .FirstOrDefault(c => c.Id == id);
    }

    public Carrinho? ObterCarrinhoPorUtilizador(int utilizadorId)
    {
        return _context.Carrinhos
            .Include(c => c.Itens)
            .ThenInclude(i => i.Filme)
            .Include(c => c.Itens)
            .ThenInclude(i => i.Festival)
            .FirstOrDefault(c => c.UtilizadorId == utilizadorId);
    }

    public Carrinho? ObterComItens(int utilizadorId)
    {
        return _context.Carrinhos
            .Include(c => c.Itens)
            .ThenInclude(i => i.Filme)
            .Include(c => c.Itens)
            .ThenInclude(i => i.Festival)
            .FirstOrDefault(c => c.UtilizadorId == utilizadorId);
    }

    public void CriarCarrinho(Carrinho carrinho)
    {
        _context.Carrinhos.Add(carrinho);
        _context.SaveChanges();
    }

    public void AtualizarCarrinho(Carrinho carrinho)
    {
        _context.Carrinhos.Update(carrinho);
        _context.SaveChanges();
    }

    public void RemoverCarrinho(int id)
    {
        var carrinho = _context.Carrinhos.Find(id);
        if (carrinho != null)
        {
            _context.Carrinhos.Remove(carrinho);
            _context.SaveChanges();
        }
    }

    public void Guardar() => _context.SaveChanges();

    public void Limpar(Carrinho carrinho)
    {
        _context.Itens.RemoveRange(carrinho.Itens);
        _context.SaveChanges();
    }
}