using System.Text.Json.Serialization;

namespace ProjetoES.API.Models.External;

public class TmdbVideoApiResponse
{
    [JsonPropertyName("results")]
    public List<TmdbVideoApiDto> Results { get; set; } = new();
}

public class TmdbVideoApiDto
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

public class TmdbMovieDetailsApiDto
{
    [JsonPropertyName("runtime")]
    public int Runtime { get; set; }

    [JsonPropertyName("genres")]
    public List<TmdbGenreApiDto> Genres { get; set; } = new();
}

public class TmdbGenreApiDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
}

public class TmdbSearchApiResponse
{
    [JsonPropertyName("results")]
    public List<TmdbMovieApiDto> Results { get; set; } = new();
}

public class TmdbMovieApiDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("overview")]
    public string Overview { get; set; } = string.Empty;

    [JsonPropertyName("release_date")]
    public string ReleaseDate { get; set; } = string.Empty;

    [JsonPropertyName("poster_path")]
    public string PosterPath { get; set; } = string.Empty;
}

public class TmdbCreditsApiResponse
{
    [JsonPropertyName("cast")]
    public List<TmdbCastMemberApiDto> Cast { get; set; } = new();

    [JsonPropertyName("crew")]
    public List<TmdbCrewMemberApiDto> Crew { get; set; } = new();
}

public class TmdbCastMemberApiDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("character")]
    public string Character { get; set; } = string.Empty;

    [JsonPropertyName("order")]
    public int Order { get; set; }
}

public class TmdbCrewMemberApiDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("job")]
    public string Job { get; set; } = string.Empty;
}