namespace ProjetoES.API.DTOs;
/// <summary>
/// DTO para filtrar festivais, permitindo a pesquisa por nome, datas e local.
/// </summary>
public class FestivalFiltroDTO
{
    public string? Nome { get; set; }
    public DateOnly? DataInicio { get; set; }
    public DateOnly? DataFim { get; set; }
    public string? Local { get; set; }
}
