using System.Text.Json.Serialization;

namespace ProjetoES.Models
{
    // Esta classe representa a resposta total da pesquisa
    public class TmdbSearchResponse
    {
        [JsonPropertyName("results")]
        public List<TmdbMovie> Resultados { get; set; } = new();
    }

    // Esta classe representa os detalhes de cada filme individual
    public class TmdbMovie
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Titulo { get; set; } = string.Empty;

        [JsonPropertyName("overview")]
        public string Sinopse { get; set; } = string.Empty;

        [JsonPropertyName("release_date")]
        public string DataLancamento { get; set; } = string.Empty;

        [JsonPropertyName("poster_path")]
        public string PosterPath { get; set; } = string.Empty;

        // Propriedade extra para gerar o link completo da imagem!
        public string PosterUrlCompleto => string.IsNullOrEmpty(PosterPath)
            ? "/images/default-poster.jpg"
            : $"https://image.tmdb.org/t/p/w500{PosterPath}";

        public class TmdbMovieDetails
        {
            [JsonPropertyName("runtime")]
            public int Duracao { get; set; }

            [JsonPropertyName("genres")]
            public List<TmdbGenre> Generos { get; set; } = new();

            // Transforma a lista de géneros numa string (Ex: "Ação, Ficção Científica")
            public string GenerosString => string.Join(", ", Generos.Select(g => g.name));
        }

        public class TmdbGenre
        {
            [JsonPropertyName("name")]
            public string name { get; set; } = string.Empty;
        }
    }
}