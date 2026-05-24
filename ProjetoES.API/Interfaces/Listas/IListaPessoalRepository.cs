using ProjetoES.API.Models;

namespace ProjetoES.API.Interfaces
{
    /// <summary>
    /// Interface para o repositório de listas pessoais, que define os métodos para a gestão de listas pessoais de filmes dos membros.
    /// </summary>
    public interface IListaPessoalRepository
    {
        ListaPessoal? ObterPorId(int id);
        IEnumerable<ListaPessoal> ObterPorMembro(int membroId);
        void Adicionar(ListaPessoal lista);
        void Atualizar(ListaPessoal lista);
        void Remover(int id);
    }
}