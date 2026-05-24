namespace ProjetoES.API.DTOs;

/// <summary>
/// DTO para a resposta de um filme, incluindo informações detalhadas como título, sinopse, gênero, ano, duração, média de avaliação, URLs de poster e trailer, realizador, elenco e os IDs dos festivais associados.
/// </summary>
public class FilmeResponseDTO
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Sinopse { get; set; } = string.Empty;
    public string Genero { get; set; } = string.Empty;
    public int Ano { get; set; }
    public int DuracaoMinutos { get; set; }
    public double MediaAvaliacao { get; set; }
    public string PosterUrl { get; set; } = string.Empty;
    public string TrailerUrl { get; set; } = string.Empty;
    public string Realizador { get; set; } = string.Empty; 
    public string Elenco { get; set; } = string.Empty;
    public List<int> FestivalIds { get; set; } = new();
}
