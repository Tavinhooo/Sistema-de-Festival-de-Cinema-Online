namespace ProjetoES.API.DTOs;
/// <summary>
/// DTO para a morada de um utilizador, incluindo o nome do destinatário, morada de faturação, código postal, localidade e país.
/// </summary>
public class MoradaDTO
{
    public string NomeDestinatario { get; set; } = string.Empty;
    public string MoradaFaturacao { get; set; } = string.Empty;
    public string CodigoPostal { get; set; } = string.Empty;
    public string Localidade { get; set; } = string.Empty;
    public string Pais { get; set; } = string.Empty;
}

