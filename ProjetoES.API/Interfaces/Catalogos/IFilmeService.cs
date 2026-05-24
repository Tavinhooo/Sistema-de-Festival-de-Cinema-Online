using ProjetoES.API.Models;

namespace ProjetoES.API.Interfaces
{
    public interface IFilmeService
    {
        Task AdicionarFilmeAsync(Filme filme);
        Task<List<Filme>> ObterTodosFilmesAsync();
        Task EliminarFilmeAsync(int id);
        Task<Filme?> ObterFilmePorIdAsync(int id);
        Task AtualizarFilmeAsync(Filme filme);
        Task<List<Filme>> ObterFilmesPorFestivalAsync(int festivalId);
    }
}