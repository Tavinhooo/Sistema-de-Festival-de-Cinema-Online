using ProjetoES.API.Interfaces;
namespace ProjetoES.API.Services;

public class FavoritoStrategyService : IListaPessoalStrategy
{
    public int LimiteMaximo => 50;
    public string NomeLista => "Favoritos";

    public void AdicionarFilme(int filmeId)
    {

    }

    public void RemoverFilme(int filmeId)
    {
       
    }
}