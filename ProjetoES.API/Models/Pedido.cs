using System.ComponentModel.DataAnnotations;

namespace ProjetoES.API.Models
{
    public enum EstadoPedido { Pendente, Completo, Cancelado }

    public class Pedido
    {
        public int Id { get; set; }
        
        public int MemberId { get; set; }
        public virtual Membro? Membro { get; set; }
        
        public DateTime DataPedido { get; set; } = DateTime.UtcNow;
        public DateTime? DataPagamento { get; set; }
        
        public double Total { get; set; }
        public EstadoPedido Estado { get; set; } = EstadoPedido.Pendente;
        
        // Itens do pedido (cópia dos itens do carrinho no momento da compra)
        public virtual ICollection<ItemPedido> Itens { get; set; } = new List<ItemPedido>();
    }
}
