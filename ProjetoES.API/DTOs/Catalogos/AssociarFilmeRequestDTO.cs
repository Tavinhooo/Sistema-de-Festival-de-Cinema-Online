namespace ProjetoES.API.DTOs.Catalogos;
/// <summary>
/// DTO para associar um filme a um catálogo, incluindo o ID do filme e o preço do bilhete (opcional).
/// </summary>
public class AssociarFilmeRequestDTO
{
    public int FilmeId { get; set; }
    public decimal? PrecoBilhete { get; set; }
}
