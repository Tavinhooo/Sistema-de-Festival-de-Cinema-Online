namespace ProjetoES.API.DTOs;

/// <summary>
/// DTO para a resposta de um preço, incluindo o tipo de acesso, descrição e preço total.
/// </summary>
public class PrecoResponseDTO
{
    public string TipoAcesso { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public decimal PrecoTotal { get; set; }
}