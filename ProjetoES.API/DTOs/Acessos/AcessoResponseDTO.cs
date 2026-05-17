namespace ProjetoES.API.DTOs;

public class AcessoResponseDTO
{
    public int Id { get; set; }
    public int FilmeId { get; set; }
    public string? FilmeTitulo { get; set; }
    public string TipoAcesso { get; set; } = string.Empty;
    public DateTime DataAquisicao { get; set; }
    public DateTime? DataValidade { get; set; }
    public string Estado { get; set; } = string.Empty;
}
