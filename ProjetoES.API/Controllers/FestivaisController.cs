using Microsoft.AspNetCore.Mvc;
using ProjetoES.API.DTOs;
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
    public ActionResult<List<FestivalResponseDTO>> ObterTodosFestivais()
    {
        List<FestivalResponseDTO> festivais = _service.ObterTodosFestivais()
            .Select(festival => new FestivalResponseDTO
            {
                Id = festival.Id,
                Nome = festival.Nome,
                DataInicio = festival.DataInicio,
                DataFim = festival.DataFim,
                Estado = festival.Estado.ToString()
            })
            .ToList();

        return Ok(festivais);
    }

    [HttpGet("a-decorrer")]
    public ActionResult<List<FestivalResponseDTO>> ObterFestivaisADecorrer()
    {
        List<FestivalResponseDTO> festivais = _service.ObterFestivaisADecorrer()
            .Select(festival => new FestivalResponseDTO
            {
                Id = festival.Id,
                Nome = festival.Nome,
                DataInicio = festival.DataInicio,
                DataFim = festival.DataFim,
                Estado = festival.Estado.ToString()
            })
            .ToList();

        return Ok(festivais);
    }

    [HttpGet("proximos")]
    public ActionResult<List<FestivalResponseDTO>> ObterFestivaisFuturos()
    {
        List<FestivalResponseDTO> festivais = _service.ObterFestivaisFuturos()
            .Select(festival => new FestivalResponseDTO
            {
                Id = festival.Id,
                Nome = festival.Nome,
                DataInicio = festival.DataInicio,
                DataFim = festival.DataFim,
                Estado = festival.Estado.ToString()
            })
            .ToList();

        return Ok(festivais);
    }

    [HttpGet("disponiveis-para-filmes")]
    public ActionResult<List<FestivalResponseDTO>> ObterFestivaisDisponiveisParaFilmes()
    {
        List<FestivalResponseDTO> festivais = _service.ObterFestivaisDisponiveisParaFilmes()
            .Select(festival => new FestivalResponseDTO
            {
                Id = festival.Id,
                Nome = festival.Nome,
                DataInicio = festival.DataInicio,
                DataFim = festival.DataFim,
                Estado = festival.Estado.ToString()
            })
            .ToList();

        return Ok(festivais);
    }

    [HttpGet("{id}")]
    public ActionResult<FestivalResponseDTO> ObterFestivalPorId(int id)
    {
        Festival? festival = _service.ObterFestivalPorId(id);
        if (festival == null)
        {
            return NotFound();
        }

        return Ok(new FestivalResponseDTO
        {
            Id = festival.Id,
            Nome = festival.Nome,
            DataInicio = festival.DataInicio,
            DataFim = festival.DataFim,
            Estado = festival.Estado.ToString()
        });
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