using System.ComponentModel.DataAnnotations;

namespace ProjetoES.API.DTOS;

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