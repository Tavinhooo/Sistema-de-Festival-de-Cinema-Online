using ProjetoES.API.Interfaces;
using ProjetoES.API.Models;
using ProjetoES.API.Services;
namespace ProjetoES.API.Factories
{
    /// <summary>
    /// Fábrica estática para criar instâncias de IListaPessoalStrategy, que representa as estratégias para as 
    /// listas pessoais dos clientes (Ver Depois, Visto e Favoritos). Esta fábrica define um método Criar,
    /// que recebe um TipoLista como parâmetro e retorna a estratégia correspondente para o tipo de lista solicitado.
    /// Se o tipo de lista for desconhecido, a fábrica lança uma exceção ArgumentException.
    /// </summary>
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