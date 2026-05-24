using Microsoft.AspNetCore.Mvc;
using ProjetoES.API.DTOs;
using ProjetoES.API.Interfaces;
using ProjetoES.API.Models;
using ProjetoES.API.Services;

namespace ProjetoES.API.Controllers;

[ApiController]
[Route("api/filmes")]
// <summary>
// Controlador para gerir os filmes, incluindo criação, atualização, remoção e associação a festivais e cálculo de preços.
// </summary>
public class FilmesController : ControllerBase
{
    private readonly FilmeService _service;
    private readonly ITmdbService _tmdbService;

    public FilmesController(FilmeService service, ITmdbService tmdbService)
    {
        _service = service;
        _tmdbService = tmdbService;
    }

    private static FilmeResponseDTO ToDto(Filme filme) => new()
    {
        Id = filme.Id,
        Titulo = filme.Titulo,
        Sinopse = filme.Sinopse,
        Genero = filme.Genero,
        Ano = filme.Ano,
        DuracaoMinutos = filme.DuracaoMinutos,
        MediaAvaliacao = filme.MediaAvaliacao,
        PosterUrl = filme.PosterUrl,
        TrailerUrl = filme.TrailerUrl, 
        Realizador = filme.Realizador, 
        Elenco = filme.Elenco,
        FestivalIds = filme.Festivais.Select(f => f.Id).ToList()
    };

    [HttpGet]
    public ActionResult<List<FilmeResponseDTO>> ObterTodosFilmes()
    {
        return Ok(_service.ObterTodosFilmes().Select(ToDto).ToList());
    }

    [HttpGet("{id}")]
    public ActionResult<FilmeResponseDTO> ObterFilmePorId(int id)
    {
        var filme = _service.ObterFilmePorId(id);
        if (filme == null) return NotFound();
        return Ok(ToDto(filme));
    }

    [HttpGet("{id}/festival/{festivalId}")]
    public ActionResult<FilmeFestivalDTO> ObterFilmePorFestival(int id, int festivalId)
    {
        var filme = _service.ObterFilmePorFestival(id, festivalId);
        if (filme == null) return NotFound();
        return Ok(filme);
    }

    [HttpGet("festival/{festivalId}")]
    public ActionResult<List<FilmeFestivalDTO>> ObterFilmesPorFestival(int festivalId)
    {
        return Ok(_service.ObterFilmesPorFestival(festivalId));
    }

    [HttpGet("tmdb/pesquisa")]
    public async Task<ActionResult<List<TmdbMovie>>> PesquisarFilmesTmdb([FromQuery] string query)
    {
        return Ok(await _tmdbService.PesquisarFilmesAsync(query));
    }

    [HttpGet("tmdb/detalhes/{tmdbId}")]
    public async Task<ActionResult<TmdbMovie.TmdbMovieDetails>> ObterDetalhesTmdb(int tmdbId)
    {
        var detalhes = await _tmdbService.ObterDetalhesFilmeAsync(tmdbId);
        if (detalhes == null) return NotFound();
        return Ok(detalhes);
    }

    // endpoint dedicado para buscar o trailer de um filme TMDB
    [HttpGet("tmdb/trailer/{tmdbId}")]
    public async Task<ActionResult<string>> ObterTrailerTmdb(int tmdbId)
    {
        var url = await _tmdbService.ObterTrailerYoutubeUrlAsync(tmdbId);
        if (url == null) return NotFound("Trailer não encontrado.");
        return Ok(new { trailerUrl = url });
    }

[HttpPost]
public async Task<ActionResult<FilmeResponseDTO>> CriarFilme([FromBody] CreateFilmeDTO dto)
{
    try
    {
        string trailerUrl = dto.TrailerUrl;
        string realizador = string.Empty;
        string elenco = string.Empty;

        if (dto.TmdbId.HasValue)
        {
            if (string.IsNullOrEmpty(trailerUrl))
                trailerUrl = await _tmdbService.ObterTrailerYoutubeUrlAsync(dto.TmdbId.Value) ?? string.Empty;

            (realizador, elenco) = await _tmdbService.ObterCreditosAsync(dto.TmdbId.Value);
        }

        var filme = new Filme
        {
            Titulo = dto.Titulo,
            Sinopse = dto.Sinopse,
            Genero = dto.Genero,
            Ano = dto.Ano,
            DuracaoMinutos = dto.DuracaoMinutos,
            PosterUrl = dto.PosterUrl,
            TrailerUrl = trailerUrl,
            Realizador = realizador,
            Elenco = elenco
        };

        _service.CriarFilme(filme, dto.FestivalId, dto.PrecoBilhete);
        return CreatedAtAction(nameof(ObterFilmePorId), new { id = filme.Id }, ToDto(filme));
    }
    catch (ArgumentException ex)
    {
        return BadRequest(ex.Message);
    }
}

    [HttpPut("{id}")]
    public ActionResult<FilmeResponseDTO> AtualizarFilme(int id, [FromBody] UpdateFilmeDTO dto)
    {
        try
        {
            var filme = new Filme
            {
                Titulo = dto.Titulo,
                Sinopse = dto.Sinopse,
                Genero = dto.Genero,
                Ano = dto.Ano,
                DuracaoMinutos = dto.DuracaoMinutos,
                PosterUrl = dto.PosterUrl,
                TrailerUrl = dto.TrailerUrl 
            };

            _service.AtualizarFilme(id, filme);

            if (dto.FestivalId.HasValue)
                _service.VincularFilmeAoFestival(id, dto.FestivalId.Value, dto.PrecoBilhete);

            var updated = _service.ObterFilmePorId(id);
            return Ok(updated == null ? null : ToDto(updated));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}/festival/{festivalId}")]
    public ActionResult VincularFilmeAoFestival(int id, int festivalId, [FromBody] VincularFilmeFestivalDTO dto)
    {
        try
        {
            _service.VincularFilmeAoFestival(id, festivalId, dto.PrecoBilhete);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}/festival/{festivalId}")]
    public ActionResult DesvincularFilmeDeFestival(int id, int festivalId)
    {
        try
        {
            _service.DesvincularFilmeDeFestival(id, festivalId);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public ActionResult EliminarFilme(int id)
    {
        var filme = _service.ObterFilmePorId(id);
        if (filme == null) return NotFound();
        _service.EliminarFilme(id);
        return NoContent();
    }
}
