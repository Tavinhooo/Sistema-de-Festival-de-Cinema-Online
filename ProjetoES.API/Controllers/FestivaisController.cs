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
    public ActionResult<List<FestivalResponseDTO>> ObterTodosFestivais(string? nome, DateOnly? dataInicio, DateOnly? dataFim, string? local)
    {
        List<FestivalResponseDTO> festivais;

        if (!string.IsNullOrWhiteSpace(nome) || dataInicio.HasValue || dataFim.HasValue || !string.IsNullOrWhiteSpace(local))
        {
            festivais = _service.FiltrarFestivais(nome, dataInicio, dataFim, local)
                .Select(festival => new FestivalResponseDTO
                {
                    Id = festival.Id,
                    Nome = festival.Nome,
                    DataInicio = festival.DataInicio,
                    DataFim = festival.DataFim,
                    Estado = festival.Estado.ToString(),
                    Local = festival.Local
                })
                .ToList();
        }
        else
        {
            festivais = _service.ObterTodosFestivais()
                .Select(festival => new FestivalResponseDTO
                {
                    Id = festival.Id,
                    Nome = festival.Nome,
                    DataInicio = festival.DataInicio,
                    DataFim = festival.DataFim,
                    Estado = festival.Estado.ToString(),
                    Local = festival.Local
                })
                .ToList();
        }

        return Ok(festivais);
    }

    [HttpGet("a-decorrer")]
    public ActionResult<List<FestivalResponseDTO>> ObterFestivaisADecorrer()
    {
        var festivais = _service.ObterFestivaisADecorrer()
            .Select(festival => new FestivalResponseDTO
            {
                Id = festival.Id,
                Nome = festival.Nome,
                DataInicio = festival.DataInicio,
                DataFim = festival.DataFim,
                Estado = festival.Estado.ToString(),
                Local = festival.Local
            })
            .ToList();

        return Ok(festivais);
    }

    [HttpGet("proximos")]
    public ActionResult<List<FestivalResponseDTO>> ObterFestivaisFuturos()
    {
        var festivais = _service.ObterFestivaisFuturos()
            .Select(festival => new FestivalResponseDTO
            {
                Id = festival.Id,
                Nome = festival.Nome,
                DataInicio = festival.DataInicio,
                DataFim = festival.DataFim,
                Estado = festival.Estado.ToString(),
                Local = festival.Local
            })
            .ToList();

        return Ok(festivais);
    }

    [HttpGet("disponiveis-para-filmes")]
    public ActionResult<List<FestivalResponseDTO>> ObterFestivaisDisponiveisParaFilmes()
    {
        var festivais = _service.ObterFestivaisDisponiveisParaFilmes()
            .Select(festival => new FestivalResponseDTO
            {
                Id = festival.Id,
                Nome = festival.Nome,
                DataInicio = festival.DataInicio,
                DataFim = festival.DataFim,
                Estado = festival.Estado.ToString(),
                Local = festival.Local
            })
            .ToList();

        return Ok(festivais);
    }

    [HttpGet("{id}")]
    public ActionResult<FestivalResponseDTO> ObterFestivalPorId(int id)
    {
        Festival? festival = _service.ObterFestivalPorId(id);
        if (festival == null)
            return NotFound();

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
            return NotFound();

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