namespace ProjetoES.API.DTOS;

public class CreateFilmeDTO
{
    public int? FestivalId { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Sinopse { get; set; } = string.Empty;
    public string Genero { get; set; } = string.Empty;
    public int Ano { get; set; }
    public int DuracaoMinutos { get; set; }
    public decimal PrecoBilhete { get; set; }
    public string PosterUrl { get; set; } = string.Empty;
}