using ProjetoES.API.Models;
using ProjetoES.API.Repositories;
namespace ProjetoES.API.Models;
public class ListaPessoal
{
    public int Id { get; set; }
    public TipoLista Tipo { get; set; }
    public int MembroId { get; set; }
    public Utilizador Utilizador { get; set; } = null!;
    public List<Filme> Filmes { get; set; } = new List<Filme>();
}