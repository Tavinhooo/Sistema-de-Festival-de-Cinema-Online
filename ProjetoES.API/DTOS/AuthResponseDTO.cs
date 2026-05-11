namespace ProjetoES.API.DTOS;

public class AuthResponseDTO
{
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
}
