using Microsoft.AspNetCore.Mvc;
using ProjetoES.API.DTOs;
using ProjetoES.API.Models;
using ProjetoES.API.Services;

namespace ProjetoES.API.Controllers;

[ApiController]
[Route("api/sessoes")]
public class SessoesController : ControllerBase
{
    private readonly SessaoService _service;

    public SessoesController(SessaoService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<List<SessaoResponseDTO>> ObterTodasSessoes()
    {
        return Ok(_service.ObterTodasSessoes());
    }

    [HttpGet("{id}")]
    public ActionResult<SessaoResponseDTO> ObterSessaoPorId(int id)
    {
        var sessao = _service.ObterSessaoPorId(id);
        if (sessao == null)
        {
            return NotFound();
        }

        return Ok(sessao);
    }

    [HttpGet("festival/{festivalId}")]
    public ActionResult<List<SessaoResponseDTO>> ObterSessoesPorFestival(int festivalId)
    {
        return Ok(_service.ObterSessoesPorFestival(festivalId));
    }

    [HttpGet("filme/{filmeId}")]
    public ActionResult<List<SessaoResponseDTO>> ObterSessoesPorFilme(int filmeId)
    {
        return Ok(_service.ObterSessoesPorFilme(filmeId));
    }

    [HttpPost]
    public ActionResult<SessaoResponseDTO> CriarSessao(SessaoRequestDTO dto)
    {
        try
        {
            var resultado = _service.CriarSessao(dto);
            return CreatedAtAction(nameof(ObterSessaoPorId), new { id = resultado.Id }, resultado);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public ActionResult<SessaoResponseDTO> AtualizarSessao(int id, SessaoRequestDTO dto)
    {
        try
        {
            var resultado = _service.AtualizarSessao(id, dto);
            return Ok(resultado);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public ActionResult EliminarSessao(int id)
    {
        var sessao = _service.ObterSessaoPorId(id);
        if (sessao == null)
        {
            return NotFound();
        }

        _service.EliminarSessao(id);
        return NoContent();
    }
}
