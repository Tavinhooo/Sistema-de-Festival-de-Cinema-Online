using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoES.API.DTOs;
using ProjetoES.API.Services;

namespace ProjetoES.API.Controllers;

[ApiController]
[Route("api/cliente")]
[Authorize(Roles = "Cliente,Administrador")] 
/// <summary>
/// Controlador para gerir as funcionalidades específicas dos Clientes, incluindo visualização do histórico de compras, acessos e gestão de avaliações.
/// </summary>
public class ClienteController : ControllerBase
{
    private readonly ClienteService _service;

    public ClienteController(ClienteService service)
    {
        _service = service;
    }

    private int ObterClienteIdDoToken()
    {
        var sub = User.FindFirstValue("sub")
            ?? User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new UnauthorizedAccessException("Token inválido.");
        return int.Parse(sub);
    }

    // GET api/cliente/compras
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

    // GET api/cliente/acessos
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

    // POST api/cliente/avaliacoes
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

    // GET api/cliente/avaliacoes
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

    // PUT api/cliente/avaliacoes/{id}
    [HttpPut("avaliacoes/{id}")]
    public ActionResult<AvaliacaoResponseDTO> EditarAvaliacao(int id, CriarAvaliacaoDTO dto)
    {
        try
        {
            var clienteId = ObterClienteIdDoToken();
            return Ok(_service.EditarAvaliacao(clienteId, id, dto));
        }
        catch (UnauthorizedAccessException ex) { return Forbid(ex.Message); }
        catch (ArgumentException ex) { return BadRequest(ex.Message); }
    }

    // DELETE api/cliente/avaliacoes/{id}
    [HttpDelete("avaliacoes/{id}")]
    public ActionResult EliminarAvaliacao(int id)
    {
        try
        {
            var clienteId = ObterClienteIdDoToken();
            _service.EliminarAvaliacao(clienteId, id);
            return NoContent();
        }
        catch (UnauthorizedAccessException ex) { return Forbid(ex.Message); }
        catch (ArgumentException ex) { return NotFound(ex.Message); }
    }
}
