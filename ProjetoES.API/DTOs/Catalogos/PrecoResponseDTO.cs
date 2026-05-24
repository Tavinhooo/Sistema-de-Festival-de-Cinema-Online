namespace ProjetoES.API.DTOs;

public class PrecoResponseDTO
{
    public string TipoAcesso { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public decimal PrecoTotal { get; set; }
}