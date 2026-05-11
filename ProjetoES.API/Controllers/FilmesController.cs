using Microsoft.AspNetCore.Mvc;
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

    [HttpGet]
    public ActionResult<List<Filme>> ObterTodosFilmes()
    {
        List<Filme> filmes = _service.ObterTodosFilmes();
        return Ok(filmes);
    }

    [HttpGet("{id}")]
    public ActionResult<Filme> ObterFilmePorId(int id)
    {
        Filme? filme = _service.ObterFilmePorId(id);
        if (filme == null)
        {
            return NotFound();
        }
        return Ok(filme);
    }

    [HttpGet("festival/{festivalId}")]
    public ActionResult<List<Filme>> ObterFilmesPorFestival(int festivalId)
    {
        List<Filme> filmes = _service.ObterFilmesPorFestival(festivalId);
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
    public ActionResult CriarFilme(Filme filme)
    {
        try
        {
            _service.CriarFilme(filme);
            return CreatedAtAction(nameof(ObterFilmePorId), new { id = filme.Id }, filme);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public ActionResult AtualizarFilme(int id, Filme filme)
    {
        try
        {
            _service.AtualizarFilme(id, filme);
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
        Filme? filme = _service.ObterFilmePorId(id);
        if (filme == null)
        {
            return NotFound();
        }
        _service.EliminarFilme(id);
        return NoContent();
    }
}
