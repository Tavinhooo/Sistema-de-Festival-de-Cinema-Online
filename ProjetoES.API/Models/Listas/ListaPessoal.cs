using System.ComponentModel.DataAnnotations.Schema;
using ProjetoES.API.Models;
using ProjetoES.API.Repositories;
namespace ProjetoES.API.Models;

/// <summary>
/// Modelo de lista pessoal, representando uma lista personalizada de filmes criada por um utilizador,
///  incluindo informações sobre o tipo de lista (ex: "Favoritos", "Para Assistir"),
///  o utilizador associado à lista e os filmes contidos na lista. As listas pessoais permitem que os utilizadores organizem
///  e gerenciem seus filmes favoritos ou aqueles que desejam assistir no futuro.
/// </summary>
public class ListaPessoal
{
    public int Id { get; set; }
    public TipoLista Tipo { get; set; }
    public int MembroId { get; set; }
    [ForeignKey("MembroId")]
    public Utilizador Utilizador { get; set; } = null!;
    public List<Filme> Filmes { get; set; } = new List<Filme>();
}