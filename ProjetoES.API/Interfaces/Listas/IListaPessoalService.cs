using ProjetoES.API.Models;

namespace ProjetoES.API.Interfaces;
/// <summary>
/// Interface para o serviço de listas pessoais, que define os métodos para a gestão de listas pessoais de filmes dos membros,
///  incluindo a criação de listas, obtenção de listas por membro, obtenção de uma lista específica, adição e remoção de filmes em uma lista,
///  mudança do tipo de lista e remoção de listas.
/// </summary>
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