namespace ProjetoES.API.Interfaces
{
    public interface IListaStrategy
    {
        void AdicionarFilme(int filmeId);
        void RemoverFilme(int filmeId);
        int LimiteMaximo { get; }
        string NomeLista { get; }
    }
}