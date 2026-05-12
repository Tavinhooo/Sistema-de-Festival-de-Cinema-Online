using System.ComponentModel.DataAnnotations;

namespace ProjetoES.API.Models
{
    public class Carrinho
    {
        public int Id { get; set; }
        
        public int UtilizadorId { get; set; }
        
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
        
        // Relação 1 para muitos com os Itens do Pedido (workflow: Carrinho → Compra → Pedido)
        public virtual ICollection<ItemPedido> Itens { get; set; } = new List<ItemPedido>();
    }
}