using System.ComponentModel.DataAnnotations;

namespace ProjetoES.API.Models
{
    /// <summary>
    /// Modelo de carrinho de compras, representando o carrinho de um cliente, incluindo informações sobre o cliente,
    ///  data de criação e os itens do pedido associados ao carrinho. O carrinho é utilizado como parte do workflow de compra,
    ///  onde os clientes adicionam filmes ao carrinho antes de finalizar a compra e criar um pedido.
    /// </summary>
    public class Carrinho
    {
        public int Id { get; set; }
        
        public int UtilizadorId { get; set; }
        
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
        
        // Relação 1 para muitos com os Itens do Pedido (workflow: Carrinho → Compra → Pedido)
        public virtual ICollection<ItemPedido> Itens { get; set; } = new List<ItemPedido>();
    }
}