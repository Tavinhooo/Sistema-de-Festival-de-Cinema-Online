namespace ProjetoES.API.DTOs;

/// <summary>
/// DTO para representar um filme em um festival, incluindo detalhes como título, sinopse, gênero, ano, duração, média de avaliação, URLs de poster e trailer, realizador, elenco, e informações do festival associado.
/// </summary>
public class FilmeFestivalDTO
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
    public int FestivalId { get; set; }
    public string FestivalNome { get; set; } = string.Empty;
    public decimal PrecoBilhete { get; set; }
}
