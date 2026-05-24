namespace ProjetoES.API.DTOs;

/// <summary>
/// DTO para a resposta de autenticação, incluindo o token JWT e a data de expiração.
/// </summary>
public class AuthResponseDTO
{
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
}

