using System.ComponentModel.DataAnnotations;

namespace ProjetoES.API.DTOs;

/// <summary>
/// DTO para o registo de novos utilizadores, incluindo validação de campos obrigatórios e formato de email.
/// </summary>
public class AuthRegisterDTO
{
    public int? VisitanteId { get; set; }
    public string PrimeiroNome { get; set; } = string.Empty;
    public string UltimoNome { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email é obrigatório.")]
    [EmailAddress(ErrorMessage = "Formato de email inválido.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password é obrigatória.")]
    public string Password { get; set; } = string.Empty;
}
