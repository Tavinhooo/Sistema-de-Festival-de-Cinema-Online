using ProjetoES.Models;

namespace ProjetoES.Interfaces
{
    public interface ITmdbService
    {
        Task<List<TmdbMovie>> PesquisarFilmesAsync(string query);
        Task<TmdbMovie.TmdbMovieDetails?> ObterDetalhesFilmeAsync(int tmdbId);
    }
}