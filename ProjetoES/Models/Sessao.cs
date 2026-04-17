namespace ProjetoES.Models;

public class Sessao
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public DateTime DataHora { get; set; }
    public string Sala { get; set; } = string.Empty;
}
