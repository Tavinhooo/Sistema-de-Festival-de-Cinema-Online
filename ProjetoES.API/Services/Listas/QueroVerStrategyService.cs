using ProjetoES.API.Interfaces;
using ProjetoES.API.Models;
namespace ProjetoES.API.Services;

public class QueroVerStrategyService : IListaPessoalStrategy
{
    public int LimiteMaximo => 100;
    public string NomeLista => "Quero Ver";

    public void AdicionarFilme(int filmeId)
    {

    }

    public void RemoverFilme(int filmeId)
    {
       
    }
}