namespace ProjetoES.API.Models
{
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
