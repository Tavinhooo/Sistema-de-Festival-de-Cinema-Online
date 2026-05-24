namespace ProjetoES.API.DTOs;

public class ItemCarrinhoResponseDTO
{
    public int Id { get; set; }
    public int? FilmeId { get; set; }
    public string FilmeTitulo { get; set; } = string.Empty;
    public int FestivalId { get; set; }
    public string FestivalNome { get; set; } = string.Empty;
    public int Quantidade { get; set; }
    public string TipoAcesso { get; set; } = string.Empty;
    public double PrecoUnitario { get; set; }
    public double PrecoOriginal { get; set; }
    public double Subtotal { get; set; }
    public bool IsFestivalPass { get; set; }
}
