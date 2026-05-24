namespace ProjetoES.API.DTOs;

/// <summary>
/// DTO para atualizar um filme, incluindo informações como título, sinopse, gênero, ano, duração, preço do bilhete, URLs de poster e trailer, e o ID do festival associado (opcional).
/// </summary>
public class UpdateFilmeDTO
{
    public string Titulo { get; set; } = string.Empty;
    public string Sinopse { get; set; } = string.Empty;
    public string Genero { get; set; } = string.Empty;
    public int Ano { get; set; }
    public int DuracaoMinutos { get; set; }
    public decimal PrecoBilhete { get; set; }
    public string PosterUrl { get; set; } = string.Empty;
    public string TrailerUrl { get; set; } = string.Empty;
    public int? FestivalId { get; set; }
}
