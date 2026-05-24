namespace ProjetoES.API.DTOs;

/// <summary>
/// DTO para a requisição de criação de um carrinho de compras, incluindo o ID do utilizador associado ao carrinho.
/// </summary>
public class CarrinhoRequestDTO
{
    public int UtilizadorId { get; set; }
}
