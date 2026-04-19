using ProjetoES.Models;

namespace ProjetoES.Interfaces
{
    public interface IFilmeService
    {
        Task AdicionarFilmeAsync(Filme filme);
        Task<List<Filme>> ObterTodosFilmesAsync();
    }
}