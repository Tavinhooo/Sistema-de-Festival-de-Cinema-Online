using ProjetoES.API.Models;

namespace ProjetoES.API.DTOs;

public class SessaoRequestDTO
{
    public int FestivalId { get; set; }
    public int FilmeId { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
    public string Sala { get; set; } = string.Empty;
    public TipoSessao Tipo { get; set; } = TipoSessao.HorarioFixo;
}
