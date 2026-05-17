namespace ProjetoES.API.Models;

public class FestivalFilme
{
    public int FestivalId { get; set; }
    public Festival Festival { get; set; } = null!;

    public int FilmeId { get; set; }
    public Filme Filme { get; set; } = null!;

    public decimal PrecoBilhete { get; set; }
}