namespace ProjetoES.API.DTOs;

/// <summary>
/// DTO para a autenticação de utilizadores.
/// </summary>
public class AuthLoginDTO
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

