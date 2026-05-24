using ProjetoES.API.Models;

namespace ProjetoES.API.Interfaces
{
    /// <summary>
    /// Interface para o serviço do TMDb, que define os métodos para pesquisar filmes, obter detalhes de filmes,
    ///  obter trailers do YouTube e obter créditos de filmes a partir do TMDb.
    /// </summary>
    public interface ITmdbService
    {
        Task<List<TmdbMovie>> PesquisarFilmesAsync(string query);
        Task<TmdbMovie.TmdbMovieDetails?> ObterDetalhesFilmeAsync(int tmdbId);
        Task<string?> ObterTrailerYoutubeUrlAsync(int tmdbId);
        Task<(string realizador, string elenco)> ObterCreditosAsync(int tmdbId);
    }
}