public class CriarPremioDTO
{
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public int FestivalId { get; set; }
    public DateTime? DataLimiteVotacao { get; set; }
}