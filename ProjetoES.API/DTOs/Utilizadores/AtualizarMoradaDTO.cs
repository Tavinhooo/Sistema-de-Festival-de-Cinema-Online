namespace ProjetoES.API.DTOs;

/// <summary>
/// DTO para a requisição de atualização da morada de um utilizador.
/// </summary>
public class AtualizarMoradaDTO
{
    public MoradaDTO Morada { get; set; } = new MoradaDTO();
}
