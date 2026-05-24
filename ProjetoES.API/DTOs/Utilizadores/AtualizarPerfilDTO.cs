namespace ProjetoES.API.DTOs;

/// <summary>
/// DTO para a requisição de atualização do perfil de um utilizador, incluindo o primeiro nome e o último nome.
/// </summary>
public class AtualizarPerfilDTO
{
    public string? PrimeiroNome { get; set; }
    public string? UltimoNome { get; set; }
}
