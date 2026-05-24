namespace ProjetoES.API.Interfaces
{
    /// <summary>
    /// Interface para a estratégia de listas, que define os métodos para adicionar e remover filmes de uma lista,
    ///  bem como as propriedades para o limite máximo de filmes e o nome da lista.
    /// </summary>
    public interface IListaStrategy
    {
        void AdicionarFilme(int filmeId);
        void RemoverFilme(int filmeId);
        int LimiteMaximo { get; }
        string NomeLista { get; }
    }
}