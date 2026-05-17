using System.Text.Json.Serialization;

namespace ProjetoES.API.Models
{
    public class TmdbSearchResponse
    {
        public List<TmdbMovie> Resultados { get; set; } = new();
    }

    public class TmdbMovie
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Sinopse { get; set; } = string.Empty;
        public string DataLancamento { get; set; } = string.Empty;
        public string PosterPath { get; set; } = string.Empty;

        public string PosterUrlCompleto => string.IsNullOrEmpty(PosterPath)
            ? "/images/default-poster.jpg"
            : $"https://image.tmdb.org/t/p/w500{PosterPath}";

        public class TmdbMovieDetails
        {
            public int Duracao { get; set; }
            public List<TmdbGenre> Generos { get; set; } = new();
            public string GenerosString => string.Join(", ", Generos.Select(g => g.name));
        }

        public class TmdbGenre
        {
            public string name { get; set; } = string.Empty;
        }

        // NOVO — Modelos para os vídeos/trailers
        public class TmdbVideoResponse
        {
            [JsonPropertyName("results")]
            public List<TmdbVideo> Results { get; set; } = new();
        }

        public class TmdbVideo
        {
            [JsonPropertyName("key")]
            public string Key { get; set; } = string.Empty;

            [JsonPropertyName("site")]
            public string Site { get; set; } = string.Empty;

            [JsonPropertyName("type")]
            public string Type { get; set; } = string.Empty;

            [JsonPropertyName("official")]
            public bool Official { get; set; }
        }
    }
}