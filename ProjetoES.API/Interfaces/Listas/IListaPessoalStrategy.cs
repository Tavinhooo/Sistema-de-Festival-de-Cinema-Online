namespace ProjetoES.API.Interfaces
{
 public interface IListaPessoalStrategy
    {
        void AdicionarFilme(int filmeId);
        void RemoverFilme(int filmeId);
        int LimiteMaximo { get; }
        string NomeLista { get; }
    }
}