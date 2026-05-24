namespace ProjetoES.API.DTOS;

/// <summary>
/// DTO para reportar uma avaliação, incluindo o motivo do reporte.
/// </summary>
public class ReportarAvaliacaoDTO
{
    public string Motivo { get; set; } = string.Empty;
}