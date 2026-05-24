namespace ProjetoES.API.Models
{
    /// <summary>
    /// Modelo de compra, representando uma compra realizada por um cliente, incluindo informações sobre o cliente, data da compra,
    ///  valor total, método de pagamento, referência de pagamento, status da compra e os itens do pedido associados à compra.
    ///  A compra é o resultado final do processo de compra, onde os clientes finalizam a compra dos filmes adicionados ao carrinho
    ///  e criam um pedido para acessar os filmes adquiridos.
    /// </summary>
    public class Compra
    {
        public int Id { get; set; }
        
        public int UtilizadorId { get; set; }

        public DateTime DataCompra { get; set; } = DateTime.UtcNow;

        public double ValorTotal { get; set; }

        public string MetodoPagamento { get; set; } = string.Empty; // "Stripe", "Paypal", etc
        
        public string? ReferenciaPagamento { get; set; } // Stripe transaction ID, PayPal reference, etc
        
        public string Status { get; set; } = "Pendente"; // "Pendente", "Concluída", "Falhada", "Cancelada"
        
        // Relação 1 para muitos com os Itens (workflow: Carrinho → Compra → Pedido)
        public virtual ICollection<ItemPedido> Itens { get; set; } = new List<ItemPedido>();
    }
}
