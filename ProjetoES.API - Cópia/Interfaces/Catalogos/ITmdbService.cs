using ProjetoES.API.Models;

namespace ProjetoES.API.Interfaces
{
    public interface ITmdbService
    {
        Task<List<TmdbMovie>> PesquisarFilmesAsync(string query);
        Task<TmdbMovie.TmdbMovieDetails?> ObterDetalhesFilmeAsync(int tmdbId);
        Task<string?> ObterTrailerYoutubeUrlAsync(int tmdbId);
        Task<(string realizador, string elenco)> ObterCreditosAsync(int tmdbId);
    }
}