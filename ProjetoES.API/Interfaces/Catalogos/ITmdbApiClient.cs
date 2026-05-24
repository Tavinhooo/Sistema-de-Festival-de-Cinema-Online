using ProjetoES.API.Models.External;

namespace ProjetoES.API.Interfaces;

/// <summary>
/// Interface para o cliente da API do TMDb, que define os métodos para buscar filmes, obter detalhes de filmes,
///  vídeos e créditos a partir do TMDb.
/// </summary>
public interface ITmdbApiClient
{
    Task<TmdbSearchApiResponse?> SearchMoviesAsync(string query, CancellationToken cancellationToken = default);
    Task<TmdbMovieDetailsApiDto?> GetMovieDetailsAsync(int tmdbId, CancellationToken cancellationToken = default);
    Task<TmdbVideoApiResponse?> GetMovieVideosAsync(int tmdbId, CancellationToken cancellationToken = default);
    Task<TmdbCreditsApiResponse?> GetMovieCreditsAsync(int tmdbId, CancellationToken cancellationToken = default);
}