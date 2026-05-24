namespace ProjetoES.API.DTOs;

/// <summary>
/// DTO para a resposta de um carrinho de compras, incluindo o ID do carrinho, ID do utilizador, data de criação, lista de itens no carrinho e o total da compra.
/// </summary>
public class CarrinhoResponseDTO
{
    public int Id { get; set; }
    public int UtilizadorId { get; set; }
    public DateTime DataCriacao { get; set; }
    public List<ItemCarrinhoResponseDTO> Itens { get; set; } = new();
    public double Total { get; set; }
}
