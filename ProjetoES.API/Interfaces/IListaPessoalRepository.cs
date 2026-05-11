using ProjetoES.API.Models;

namespace ProjetoES.API.Interfaces
{
    public interface IListaPessoalRepository
    {
        ListaPessoal? ObterPorId(int id);
        IEnumerable<ListaPessoal> ObterPorMembro(int membroId);
        void Adicionar(ListaPessoal lista);
        void Atualizar(ListaPessoal lista);
        void Remover(int id);
    }
}