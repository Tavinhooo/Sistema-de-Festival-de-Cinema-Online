using ProjetoES.API.Interfaces;
using ProjetoES.API.Models;
namespace ProjetoES.API.Services;

public class VerDepoisStrategyService : IListaPessoalStrategy
{
    public int LimiteMaximo => 100;
    public string NomeLista => "Ver Depois";

    public void AdicionarFilme(int filmeId)
    {

    }

    public void RemoverFilme(int filmeId)
    {
       
    }
}