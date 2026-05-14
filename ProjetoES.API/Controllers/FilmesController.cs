using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoES.API.DTOS;
using ProjetoES.API.Interfaces;
using ProjetoES.API.Models;
using ProjetoES.API.Services;

namespace ProjetoES.API.Controllers;

[ApiController]
[Route("api/filmes")]
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
        FestivalIds = filme.Festivais.Select(festival => festival.Id).ToList()
    };

    [HttpGet]
    public ActionResult<List<FilmeResponseDTO>> ObterTodosFilmes()
    {
        List<FilmeResponseDTO> filmes = _service.ObterTodosFilmes().Select(ToDto).ToList();
        return Ok(filmes);
    }

    [HttpGet("{id}")]
    public ActionResult<FilmeResponseDTO> ObterFilmePorId(int id)
    {
        Filme? filme = _service.ObterFilmePorId(id);
        if (filme == null)
        {
            return NotFound();
        }

        return Ok(ToDto(filme));
    }

    [HttpGet("{id}/festival/{festivalId}")]
    public ActionResult<FilmeFestivalDTO> ObterFilmePorFestival(int id, int festivalId)
    {
        var filme = _service.ObterFilmePorFestival(id, festivalId);
        if (filme == null)
        {
            return NotFound();
        }

        return Ok(filme);
    }

    [HttpGet("festival/{festivalId}")]
    public ActionResult<List<FilmeFestivalDTO>> ObterFilmesPorFestival(int festivalId)
    {
        List<FilmeFestivalDTO> filmes = _service.ObterFilmesPorFestival(festivalId);
        return Ok(filmes);
    }

    [HttpGet("tmdb/pesquisa")]
    public async Task<ActionResult<List<TmdbMovie>>> PesquisarFilmesTmdb([FromQuery] string query)
    {
        var resultados = await _tmdbService.PesquisarFilmesAsync(query);
        return Ok(resultados);
    }

    [HttpGet("tmdb/detalhes/{tmdbId}")]
    public async Task<ActionResult<TmdbMovie.TmdbMovieDetails>> ObterDetalhesTmdb(int tmdbId)
    {
        var detalhes = await _tmdbService.ObterDetalhesFilmeAsync(tmdbId);
        if (detalhes == null)
        {
            return NotFound();
        }

        return Ok(detalhes);
    }

    [HttpPost]
    [Authorize(Roles = "Administrador")]
    public ActionResult<FilmeResponseDTO> CriarFilme([FromBody] CreateFilmeDTO dto)
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
                PosterUrl = dto.PosterUrl
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
    [Authorize(Roles = "Administrador")]
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
                PosterUrl = dto.PosterUrl
            };

            _service.AtualizarFilme(id, filme);

            if (dto.FestivalId.HasValue)
            {
                _service.VincularFilmeAoFestival(id, dto.FestivalId.Value, dto.PrecoBilhete);
            }

            var updatedFilme = _service.ObterFilmePorId(id);
            return Ok(updatedFilme == null ? null : ToDto(updatedFilme));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}/festival/{festivalId}")]
    [Authorize(Roles = "Administrador")]
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
    [Authorize(Roles = "Administrador")]
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
    [Authorize(Roles = "Administrador")]
    public ActionResult EliminarFilme(int id)
    {
        Filme? filme = _service.ObterFilmePorId(id);
        if (filme == null)
        {
            return NotFound();
        }

        _service.EliminarFilme(id);
        return NoContent();
    }
}
