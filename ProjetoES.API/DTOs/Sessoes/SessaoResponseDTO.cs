using ProjetoES.API.Models;

namespace ProjetoES.API.DTOs;

/// <summary>
/// DTO para a resposta de uma sessão, incluindo o ID da sessão, ID do festival, ID do filme, datas de início e fim, e informações sobre a sala e o tipo de sessão.
/// </summary>
public class SessaoResponseDTO
{
    public int Id { get; set; }
    public int FestivalId { get; set; }
    public string FestivalNome { get; set; } = string.Empty;
    public int FilmeId { get; set; }
    public string FilmeTitulo { get; set; } = string.Empty;
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
    public string Sala { get; set; } = string.Empty;
    public TipoSessao Tipo { get; set; }
}
