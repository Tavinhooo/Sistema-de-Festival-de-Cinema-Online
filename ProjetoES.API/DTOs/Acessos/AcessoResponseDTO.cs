namespace ProjetoES.API.DTOs;

/// <summary>
/// DTO de resposta para os acessos dos utilizadores a filmes e festivais, utilizado para exibir o histórico de acessos no perfil do cliente.
/// Inclui informações como o ID do acesso, ID do filme, ID do festival, título do filme, URL do poster, tipo de acesso (compra ou acesso gratuito), data de aquisição, data de validade e estado do acesso (ativo, expirado, etc.).
/// </summary>
public class AcessoResponseDTO
{
    public int Id { get; set; }
    public int FilmeId { get; set; }
    public int FestivalId { get; set; }
    public string? FilmeTitulo { get; set; }
    public string? PosterUrl { get; set; }
    public string TipoAcesso { get; set; } = string.Empty;
    public DateTime DataAquisicao { get; set; }
    public DateTime? DataValidade { get; set; }
    public string Estado { get; set; } = string.Empty;
}
