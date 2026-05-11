namespace ProjetoES.API.DTOS;

public class ItemCarrinhoRequestDTO
{
    public int FilmeId { get; set; }
    public int Quantidade { get; set; } = 1;
    public string TipoAcesso { get; set; } = "Aluguer Digital";
}