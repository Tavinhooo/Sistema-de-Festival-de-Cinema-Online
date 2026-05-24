using ProjetoES.API.Interfaces;
using ProjetoES.API.Models;
using ProjetoES.API.Services;
namespace ProjetoES.API.Factories
{
    public static class ListaPessoalFactory
    {
        public static IListaPessoalStrategy Criar(TipoLista tipo)
        {
            return tipo switch
            {
                TipoLista.VerDepois => new VerDepoisStrategyService(),
                TipoLista.Visto => new VistoStrategyService(),
                TipoLista.Favoritos => new FavoritoStrategyService(),
                _ => throw new ArgumentException("Tipo de lista desconhecido")
            };
        }
    }
}