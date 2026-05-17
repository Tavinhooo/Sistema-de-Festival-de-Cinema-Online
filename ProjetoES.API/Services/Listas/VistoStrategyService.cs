using ProjetoES.API.Interfaces;
namespace ProjetoES.API.Services;

public class VistoStrategyService : IListaPessoalStrategy
{
    public int LimiteMaximo => 500;
    public string NomeLista => "Visto";

    public void AdicionarFilme(int filmeId)
    {

    }

    public void RemoverFilme(int filmeId)
    {
       
    }
}