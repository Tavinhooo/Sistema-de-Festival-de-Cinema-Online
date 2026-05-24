namespace ProjetoES.API.DTOs;
/// <summary>
/// DTOs relacionados ao histórico de compras, incluindo detalhes do pedido, itens do pedido e informações sobre o estado do pedido, datas de pedido e pagamento, e o preço total da compra.
/// </summary>
public class HistoricoComprasDTO
{
    public int PedidoId { get; set; }
    public DateTime DataPedido { get; set; }
    public DateTime? DataPagamento { get; set; }
    public double PrecoTotal { get; set; }
    public string Estado { get; set; } = string.Empty;
    public List<ItemPedidoDTO> Itens { get; set; } = new();
}

public class ItemPedidoDTO
{
    public int FilmeId { get; set; }
    public string? FilmeTitulo { get; set; }
    public string TipoAcesso { get; set; } = string.Empty;
    public int Quantidade { get; set; }
    public double PrecoUnitario { get; set; }
}
