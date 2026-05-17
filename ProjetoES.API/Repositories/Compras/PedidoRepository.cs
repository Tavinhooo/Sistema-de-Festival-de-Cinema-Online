using ProjetoES.API.Data;
using ProjetoES.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ProjetoES.API.Repositories
{
    public class PedidoRepository
    {
        private readonly AppDbContext _context;

        public PedidoRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Pedido> ObterTodosPedidos()
        {
            return _context.Pedidos
                .Include(p => p.Itens)
                    .ThenInclude(ip => ip.Filme)
                .Include(p => p.Utilizador)
                .ToList();
        }

        public Pedido? ObterPedidoPorId(int id)
        {
            return _context.Pedidos
                .Include(p => p.Itens)
                    .ThenInclude(ip => ip.Filme)
                .Include(p => p.Utilizador)
                .FirstOrDefault(p => p.Id == id);
        }

        public List<Pedido> ObterPedidosPorMembro(int utilizadorId)
        {
            return _context.Pedidos
                .Include(p => p.Itens)
                    .ThenInclude(ip => ip.Filme)
                .Where(p => p.UtilizadorId == utilizadorId)
                .OrderByDescending(p => p.DataPedido)
                .ToList();
        }

        public void CriarPedido(Pedido pedido)
        {
            _context.Pedidos.Add(pedido);
            _context.SaveChanges();
        }

        public void AtualizarPedido(Pedido pedido)
        {
            _context.Pedidos.Update(pedido);
            _context.SaveChanges();
        }
    }
}
