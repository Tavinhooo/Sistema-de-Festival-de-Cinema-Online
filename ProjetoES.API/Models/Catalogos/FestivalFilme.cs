namespace ProjetoES.API.Models;
/// <summary>
/// Modelo de associação entre Festival e Filme, representando a relação muitos-para-muitos entre festivais e filmes,
///  incluindo informações sobre o preço do bilhete para assistir ao filme no festival.
/// </summary>
public class FestivalFilme
{
    public int FestivalId { get; set; }
    public Festival Festival { get; set; } = null!;

    public int FilmeId { get; set; }
    public Filme Filme { get; set; } = null!;

    public decimal PrecoBilhete { get; set; }
}