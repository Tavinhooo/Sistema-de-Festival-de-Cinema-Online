namespace ProjetoES.API.DTOS;

public class CarrinhoResponseDTO
{
    public int Id { get; set; }
    public int UtilizadorId { get; set; }
    public DateTime DataCriacao { get; set; }
    public List<ItemCarrinhoResponseDTO> Itens { get; set; } = new();
    public double Total { get; set; }
}