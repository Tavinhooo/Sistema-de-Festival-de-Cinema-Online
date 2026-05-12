namespace ProjetoES.API.DTOS;

public class MembroPerfilDTO
{
    public int Id { get; set; }
    public string PrimeiroNome { get; set; } = string.Empty;
    public string UltimoNome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public string? MetodoPagamento { get; set; }
    public string? MoradaFaturacao { get; set; }
}