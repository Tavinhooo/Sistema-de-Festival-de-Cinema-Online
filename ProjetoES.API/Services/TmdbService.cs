using System.Text.Json;
using ProjetoES.API.Interfaces;
using ProjetoES.API.Models;

namespace ProjetoES.API.Services
{
    public class TmdbService : ITmdbService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        // Injetamos o HttpClient (para fazer o pedido à internet) e a Configuração (para ler a chave)
        public TmdbService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            // Lê o URL base e a chave do appsettings.json
            _httpClient.BaseAddress = new Uri(config["TmdbSettings:BaseUrl"]!);
            _apiKey = config["TmdbSettings:ApiKey"]!;
        }

        public async Task<List<TmdbMovie>> PesquisarFilmesAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query)) return new List<TmdbMovie>();

            // Constrói o URL de pesquisa com a linguagem em Português!
            var url = $"search/movie?api_key={_apiKey}&query={Uri.EscapeDataString(query)}&language=pt-PT";

            var resposta = await _httpClient.GetAsync(url);

            if (!resposta.IsSuccessStatusCode) return new List<TmdbMovie>();

            var json = await resposta.Content.ReadAsStringAsync();
            var tmdbResponse = JsonSerializer.Deserialize<TmdbSearchResponse>(json);

            return tmdbResponse?.Resultados ?? new List<TmdbMovie>();
        }
        public async Task<TmdbMovie.TmdbMovieDetails?> ObterDetalhesFilmeAsync(int tmdbId)
        {
            // Vai ao endpoint /movie/{id} buscar tudo sobre o filme!
            var url = $"movie/{tmdbId}?api_key={_apiKey}&language=pt-PT";

            var resposta = await _httpClient.GetAsync(url);
            if (!resposta.IsSuccessStatusCode) return null;

            var json = await resposta.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TmdbMovie.TmdbMovieDetails>(json);
        }
    }
}