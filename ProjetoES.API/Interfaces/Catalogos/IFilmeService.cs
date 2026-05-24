using ProjetoES.API.Models;

namespace ProjetoES.API.Interfaces
{
    /// <summary>
    /// Interface para o serviço de filme, que define os métodos para a gestão de filmes, incluindo a adição,
    ///  obtenção, eliminação e atualização de filmes, bem como a obtenção de filmes por festival.
    /// </summary>
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