/// <summary>
/// DTO para a requisição de criação de um prémio, incluindo o nome, descrição, ID do festival e data limite para votação.
/// </summary>
public class CriarPremioDTO
{
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public int FestivalId { get; set; }
    public DateTime? DataLimiteVotacao { get; set; }
}