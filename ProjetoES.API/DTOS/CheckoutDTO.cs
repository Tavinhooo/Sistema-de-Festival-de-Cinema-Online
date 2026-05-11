namespace ProjetoES.API.DTOs
{
    public class CheckoutRequestDTO
    {
        public int CarrinhoId { get; set; }
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
        public double Total { get; set; }
        public string Estado { get; set; } = string.Empty;
        public List<ItemPedidoResponseDTO> Itens { get; set; } = new();
    }
}
