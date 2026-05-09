using Microsoft.AspNetCore.Mvc;
using ProjetoES.API.Models;
using ProjetoES.API.Services;

namespace ProjetoES.API.Controllers;
[ApiController]
[Route("api/festivais")]

public class FestivaisController : ControllerBase
{
    private readonly FestivalService _service;

    public FestivaisController(FestivalService service)
    {
        _service = service;
    }
    [HttpGet]
    public ActionResult<List<Festival>> ObterTodosFestivais()
    {
        List<Festival> festivais = _service.ObterTodosFestivais();
        return Ok(festivais);
    }
    [HttpGet("{id}")]
    public ActionResult<Festival> ObterFestivalPorId(int id)
    {
        Festival? festival = _service.ObterFestivalPorId(id);
        if (festival == null)
        {
            return NotFound();
        }
        return Ok(festival);
    }
    [HttpPost]
    public ActionResult CriarFestival(Festival festival)
    {
        try
        {
            _service.CriarFestival(festival);
            return CreatedAtAction(nameof(ObterFestivalPorId), new { id = festival.Id }, festival);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpDelete("{id}")]
    public ActionResult RemoverFestival(int id)
    {
        Festival? festival = _service.ObterFestivalPorId(id);
        if (festival == null)
        {
            return NotFound();
        }
        _service.RemoverFestival(id);
        return NoContent();
    }
    [HttpPut("{id}")]
    public ActionResult AtualizarFestival(int id, Festival festival)
    {
        try
        {
            _service.AtualizarFestival(id, festival);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}