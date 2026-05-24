namespace ProjetoES.API.Models
{
    /// <summary>
    /// Modelo de item do pedido, representando um item dentro de um pedido de compra, incluindo informações sobre o filme,
    ///  quantidade, tipo de acesso e preço unitário. O item do pedido é utilizado como parte do workflow de compra,
    ///  onde os clientes adicionam filmes ao carrinho antes de finalizar a compra e criar um pedido.
    /// </summary>
    public class ItemPedido
    {
        public int Id { get; set; }
        
        public int? FilmeId { get; set; }
        public virtual Filme? Filme { get; set; }

        public int FestivalId { get; set; }
        public virtual Festival? Festival { get; set; }
        
        public int Quantidade { get; set; } = 1;

        public string TipoAcesso { get; set; } = string.Empty; // "Aluguel", "Compra", "Passe"

        public double PrecoUnitario { get; set; }
        
        // Status determines the workflow stage
        public string Status { get; set; } = "Carrinho"; // "Carrinho", "Compra", "Pedido", "Entregue", "Cancelada"
        
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
        
        // Foreign keys - nullable to support the workflow
        public int? CarrinhoId { get; set; }
        public int? CompraId { get; set; }
        public int? PedidoId { get; set; }
        
        // Navigation properties
        public virtual Carrinho? Carrinho { get; set; }
        public virtual Compra? Compra { get; set; }
        public virtual Pedido? Pedido { get; set; }
    }
}
