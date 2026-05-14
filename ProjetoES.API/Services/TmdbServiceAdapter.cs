using ProjetoES.API.Interfaces;
using ProjetoES.API.Models;

namespace ProjetoES.API.Services;

public class TmdbServiceAdapter : ITmdbService
{
    private readonly ITmdbApiClient _tmdbApiClient;

    public TmdbServiceAdapter(ITmdbApiClient tmdbApiClient)
    {
        _tmdbApiClient = tmdbApiClient;
    }

    public async Task<List<TmdbMovie>> PesquisarFilmesAsync(string query)
    {
        var response = await _tmdbApiClient.SearchMoviesAsync(query);
        if (response == null) return new List<TmdbMovie>();

        return response.Results.Select(movie => new TmdbMovie
        {
            Id = movie.Id,
            Titulo = movie.Title,
            Sinopse = movie.Overview,
            DataLancamento = movie.ReleaseDate,
            PosterPath = movie.PosterPath
        }).ToList();
    }

    public async Task<TmdbMovie.TmdbMovieDetails?> ObterDetalhesFilmeAsync(int tmdbId)
    {
        var details = await _tmdbApiClient.GetMovieDetailsAsync(tmdbId);
        if (details == null) return null;

        return new TmdbMovie.TmdbMovieDetails
        {
            Duracao = details.Runtime,
            Generos = details.Genres.Select(g => new TmdbMovie.TmdbGenre { name = g.Name }).ToList()
        };
    }

    // NOVO
    public async Task<string?> ObterTrailerYoutubeUrlAsync(int tmdbId)
    {
        var videoResponse = await _tmdbApiClient.GetMovieVideosAsync(tmdbId);
        if (videoResponse == null) return null;

        // Prioridade: trailer oficial > qualquer trailer > qualquer vídeo
        var trailer = videoResponse.Results
            .Where(v => v.Site == "YouTube" && v.Type == "Trailer" && v.Official)
            .FirstOrDefault()
            ?? videoResponse.Results
               .Where(v => v.Site == "YouTube" && v.Type == "Trailer")
               .FirstOrDefault()
            ?? videoResponse.Results
               .Where(v => v.Site == "YouTube")
               .FirstOrDefault();

        if (trailer == null) return null;

        return $"https://www.youtube.com/embed/{trailer.Key}";
    }
}