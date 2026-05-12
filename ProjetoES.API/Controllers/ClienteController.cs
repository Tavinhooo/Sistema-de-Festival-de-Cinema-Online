using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoES.API.DTOS;
using ProjetoES.API.Services;

namespace ProjetoES.API.Controllers;

[ApiController]
[Route("api/cliente")]
[Authorize(Roles = "Cliente,Administrador")] // RNF01.3 — bloqueia Visitantes e Membros
public class ClienteController : ControllerBase
{
    private readonly ClienteService _service;

    public ClienteController(ClienteService service)
    {
        _service = service;
    }

    private int ObterClienteIdDoToken()
    {
        var sub = User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new UnauthorizedAccessException("Token inválido.");
        return int.Parse(sub);
    }

    // GET api/cliente/compras — RU10 / RF07
    [HttpGet("compras")]
    public ActionResult<List<HistoricoComprasDTO>> ObterHistoricoCompras()
    {
        try
        {
            var id = ObterClienteIdDoToken();
            return Ok(_service.ObterHistoricoCompras(id));
        }
        catch (ArgumentException ex) { return NotFound(ex.Message); }
    }

    // GET api/cliente/acessos — RU09
    [HttpGet("acessos")]
    public ActionResult<List<AcessoResponseDTO>> ObterAcessos()
    {
        try
        {
            var id = ObterClienteIdDoToken();
            return Ok(_service.ObterAcessos(id));
        }
        catch (ArgumentException ex) { return NotFound(ex.Message); }
    }

    // POST api/cliente/avaliacoes — RF13 / RU08
    [HttpPost("avaliacoes")]
    public ActionResult<AvaliacaoResponseDTO> CriarAvaliacao(CriarAvaliacaoDTO dto)
    {
        try
        {
            var id = ObterClienteIdDoToken();
            return Created(string.Empty, _service.CriarAvaliacao(id, dto));
        }
        catch (ArgumentException ex) { return BadRequest(ex.Message); }
    }

    // GET api/cliente/avaliacoes — RU08
    [HttpGet("avaliacoes")]
    public ActionResult<List<AvaliacaoResponseDTO>> ObterAvaliacoes()
    {
        try
        {
            var id = ObterClienteIdDoToken();
            return Ok(_service.ObterAvaliacoes(id));
        }
        catch (ArgumentException ex) { return NotFound(ex.Message); }
    }
}