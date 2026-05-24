using System.Text.Json;
using ProjetoES.API.Interfaces;
using ProjetoES.API.Models.External;

namespace ProjetoES.API.Services;

public class TmdbApiClient : ITmdbApiClient
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public TmdbApiClient(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(config["TmdbSettings:BaseUrl"]!);
        _apiKey = config["TmdbSettings:ApiKey"]!;
    }

    public async Task<TmdbSearchApiResponse?> SearchMoviesAsync(string query, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(query))
            return new TmdbSearchApiResponse();

        var url = $"search/movie?api_key={_apiKey}&query={Uri.EscapeDataString(query)}&language=pt-PT";
        var response = await _httpClient.GetAsync(url, cancellationToken);

        if (!response.IsSuccessStatusCode)
            return new TmdbSearchApiResponse();

        var json = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<TmdbSearchApiResponse>(json);
    }

    public async Task<TmdbMovieDetailsApiDto?> GetMovieDetailsAsync(int tmdbId, CancellationToken cancellationToken = default)
    {
        var url = $"movie/{tmdbId}?api_key={_apiKey}&language=pt-PT";
        var response = await _httpClient.GetAsync(url, cancellationToken);

        if (!response.IsSuccessStatusCode)
            return null;

        var json = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<TmdbMovieDetailsApiDto>(json);
    }

    public async Task<TmdbVideoApiResponse?> GetMovieVideosAsync(int tmdbId, CancellationToken cancellationToken = default)
    {
        var url = $"movie/{tmdbId}/videos?api_key={_apiKey}&language=en-US";
        var response = await _httpClient.GetAsync(url, cancellationToken);

        if (!response.IsSuccessStatusCode)
            return null;

        var json = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<TmdbVideoApiResponse>(json);
    }
    public async Task<TmdbCreditsApiResponse?> GetMovieCreditsAsync(int tmdbId, CancellationToken cancellationToken = default)
    {
        var url = $"movie/{tmdbId}/credits?api_key={_apiKey}&language=pt-PT";
        var response = await _httpClient.GetAsync(url, cancellationToken);

        if (!response.IsSuccessStatusCode)
            return null;

        var json = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<TmdbCreditsApiResponse>(json);
    }
}