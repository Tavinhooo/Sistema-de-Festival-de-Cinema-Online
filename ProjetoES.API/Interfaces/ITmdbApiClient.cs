using ProjetoES.API.Models.External;

namespace ProjetoES.API.Interfaces;

public interface ITmdbApiClient
{
    Task<TmdbSearchApiResponse?> SearchMoviesAsync(string query, CancellationToken cancellationToken = default);
    Task<TmdbMovieDetailsApiDto?> GetMovieDetailsAsync(int tmdbId, CancellationToken cancellationToken = default);
}