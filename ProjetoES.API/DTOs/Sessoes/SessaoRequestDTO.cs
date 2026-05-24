using ProjetoES.API.Models;

namespace ProjetoES.API.DTOs;

/// <summary>
/// DTO para a requisição de uma sessão, incluindo o ID do festival, ID do filme, datas de início e fim, e informações sobre a sala e o tipo de sessão.
/// </summary>
public class SessaoRequestDTO
{
    public int FestivalId { get; set; }
    public int FilmeId { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
    public string Sala { get; set; } = string.Empty;
    public TipoSessao Tipo { get; set; } = TipoSessao.HorarioFixo;
}
