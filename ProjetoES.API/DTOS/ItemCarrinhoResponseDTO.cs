namespace ProjetoES.API.DTOS;

public class ItemCarrinhoResponseDTO
{
    public int Id { get; set; }
    public int FilmeId { get; set; }
    public string FilmeTitulo { get; set; } = string.Empty;
    public int Quantidade { get; set; }
    public double PrecoUnitario { get; set; }
    public double Subtotal { get; set; }
}