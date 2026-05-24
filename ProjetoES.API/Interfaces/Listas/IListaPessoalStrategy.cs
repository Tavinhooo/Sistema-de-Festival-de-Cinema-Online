namespace ProjetoES.API.Interfaces
{
    /// <summary>
    /// Interface para a estratégia de listas pessoais, que define os métodos para adicionar e remover filmes de uma lista pessoal,
    ///  bem como as propriedades para o limite máximo de filmes e o nome da lista.
    /// </summary>
 public interface IListaPessoalStrategy
    {
        void AdicionarFilme(int filmeId);
        void RemoverFilme(int filmeId);
        int LimiteMaximo { get; }
        string NomeLista { get; }
    }
}