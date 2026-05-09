using Microsoft.AspNetCore.Mvc;
using ProjetoES.API.Models;
using ProjetoES.API.Services;

namespace ProjetoES.API.Controllers;

[ApiController]
[Route("api/filmes")]
public class FilmesController : ControllerBase
{
    private readonly FilmeService _service;

    public FilmesController(FilmeService service)
    {
        _service = service;
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
