using System.ComponentModel.DataAnnotations;

namespace ProjetoES.API.DTOs;
/// <summary>
/// DTO para a requisição de um item no carrinho de compras, incluindo o ID do filme, ID do festival, quantidade do item e tipo de acesso (aluguer digital ou bilhete físico).
/// </summary>
public class ItemCarrinhoRequestDTO
{
    public int? FilmeId { get; set; }

    [Required]
    public int FestivalId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser pelo menos 1")]
    public int Quantidade { get; set; } = 1;

    [Required]
    [StringLength(50)]
    public string TipoAcesso { get; set; } = "Aluguer Digital";

}
