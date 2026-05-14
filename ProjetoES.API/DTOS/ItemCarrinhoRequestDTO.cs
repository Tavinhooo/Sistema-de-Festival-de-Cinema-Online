using System.ComponentModel.DataAnnotations;

namespace ProjetoES.API.DTOS;

public class ItemCarrinhoRequestDTO
{
    [Required]
    public int FilmeId { get; set; }

    [Required]
    public int FestivalId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser pelo menos 1")]
    public int Quantidade { get; set; } = 1;

    [Required]
    [StringLength(50)]
    public string TipoAcesso { get; set; } = "Aluguer Digital";
}