namespace ProjetoES.API.DTOs;
/// <summary>
/// DTO para a resposta do perfil de um membro, incluindo o ID, primeiro nome, último nome, email, tipo de utilizador, método de pagamento e morada de faturação (se aplicável).
/// </summary>
public class MembroPerfilDTO
{
    public int Id { get; set; }
    public string PrimeiroNome { get; set; } = string.Empty;
    public string UltimoNome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public string? MetodoPagamento { get; set; }
    public MoradaDTO? MoradaFaturacao { get; set; }
}
