using ProjetoES.API.Models;

namespace ProjetoES.API.Interfaces;

public interface IListaPessoalService
{
    ListaPessoal CriarLista(int membroId, TipoLista tipo);
    IEnumerable<ListaPessoal> ObterPorMembro(int membroId);
    ListaPessoal? ObterLista(int listaId);
    void AdicionarFilme(int listaId, int filmeId);
    void RemoverFilme(int listaId, int filmeId);
    void MudarTipoLista(int listaId, TipoLista novoTipo);
    void RemoverLista(int id);
}