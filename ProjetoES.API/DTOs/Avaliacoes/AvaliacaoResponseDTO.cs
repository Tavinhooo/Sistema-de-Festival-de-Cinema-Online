namespace ProjetoES.API.DTOs;

public class AvaliacaoResponseDTO
{
    public int Id { get; set; }
    public int FilmeId { get; set; }
    public int ClienteId { get; set; }
    public string? FilmeTitulo { get; set; }
    public string? ClienteNome { get; set; }
    public int Classificacao { get; set; }
    public string Comentario { get; set; } = string.Empty;

    public DateTime DataAvaliacao { get; set; }
    public string? MotivoReporte { get; set; }
}
