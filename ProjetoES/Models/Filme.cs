namespace ProjetoES.Models;

public class Filme
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Genero { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public DateTime DataLancamento { get; set; }
}
