namespace ProjetoES.API.DTOS;

public class HistoricoComprasDTO
{
    public int PedidoId { get; set; }
    public DateTime DataPedido { get; set; }
    public DateTime? DataPagamento { get; set; }
    public double Total { get; set; }
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