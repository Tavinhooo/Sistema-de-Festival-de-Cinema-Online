namespace ProjetoES.API.DTOS;

public class AvaliacaoResponseDTO
{
    public int Id { get; set; }
    public int FilmeId { get; set; }
    public string? FilmeTitulo { get; set; }
    public int Classificacao { get; set; }
    public string Comentario { get; set; } = string.Empty;
    public DateTime DataAvaliacao { get; set; }
}