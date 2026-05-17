namespace ProjetoES.API.DTOs;

public class FestivalFiltroDTO
{
    public string? Nome { get; set; }
    public DateOnly? DataInicio { get; set; }
    public DateOnly? DataFim { get; set; }
    public string? Local { get; set; }
}
