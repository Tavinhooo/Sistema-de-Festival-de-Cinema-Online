using System.ComponentModel.DataAnnotations;

namespace ProjetoES.API.DTOS;

public class CriarAvaliacaoDTO
{
    public int FilmeId { get; set; }

    // RF13: escala 1 a 10
    [Range(1, 10, ErrorMessage = "A Classificacao deve estar entre 1 e 10.")]
    public int Classificacao { get; set; }

    public string Comentario { get; set; } = string.Empty;
}