using System.ComponentModel.DataAnnotations;

/// <summary>
/// DTOs relacionados ao processo de checkout, incluindo a requisição de checkout, resposta de itens do pedido, resposta do pedido e resultado do checkout com promoção automática de Membro para Cliente.
/// </summary>
namespace ProjetoES.API.DTOs
{
    public class CheckoutRequestDTO
    {
        public int CarrinhoId { get; set; }

        [Required]
        [StringLength(50)]
        public string MetodoPagamento { get; set; } = string.Empty;
    }

    public class ItemPedidoResponseDTO
    {
        public int Id { get; set; }
        public int FilmeId { get; set; }
        public string FilmeTitulo { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public double PrecoUnitario { get; set; }
        public double Subtotal => PrecoUnitario * Quantidade;
    }

    public class PedidoResponseDTO
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public DateTime DataPedido { get; set; }
        public DateTime? DataPagamento { get; set; }
        public double PrecoTotal { get; set; }
        public string Estado { get; set; } = string.Empty;
        public List<ItemPedidoResponseDTO> Itens { get; set; } = new();
    }

    // RF04 — resultado do checkout com promoção automática de Membro para Cliente
    public class CheckoutResultDTO
    {
        public int PedidoId { get; set; }
        public double PrecoTotal { get; set; }
        public string Estado { get; set; } = string.Empty;
        // Preenchido apenas se foi promovido de Membro para Cliente
        public string? NovoToken { get; set; }
        public DateTime? TokenExpiresAt { get; set; }
    }
}
