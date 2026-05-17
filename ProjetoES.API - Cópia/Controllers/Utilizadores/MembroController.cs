using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoES.API.DTOs;
using ProjetoES.API.Services;

namespace ProjetoES.API.Controllers;

[ApiController]
[Route("api/membro")]
[Authorize(Roles = "Membro,Cliente,Administrador")] // RF05.1 — bloqueia Visitantes
public class MembroController : ControllerBase
{
    private readonly MembroService _service;

    public MembroController(MembroService service)
    {
        _service = service;
    }

    // Extrai o ID do utilizador autenticado a partir do JWT
    private int ObterMembroIdDoToken()
    {
        // ASP.NET Core mapeia "sub" para NameIdentifier automaticamente
        var sub = User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new UnauthorizedAccessException("Token inválido.");
        return int.Parse(sub);
    }

    // GET api/membro/perfil
    [HttpGet("perfil")]
    public ActionResult<MembroPerfilDTO> ObterPerfil()
    {
        try
        {
            var id = ObterMembroIdDoToken();
            return Ok(_service.ObterPerfil(id));
        }
        catch (ArgumentException ex) { return NotFound(ex.Message); }
    }

    // PATCH api/membro/perfil
    [HttpPatch("perfil")]
    public ActionResult<MembroPerfilDTO> AtualizarPerfil(AtualizarPerfilDTO dto)
    {
        try
        {
            var id = ObterMembroIdDoToken();
            return Ok(_service.AtualizarPerfil(id, dto));
        }
        catch (ArgumentException ex) { return BadRequest(ex.Message); }
    }

    // PUT api/membro/morada — RU06
    [HttpPut("morada")]
    public ActionResult<MembroPerfilDTO> AtualizarMorada(AtualizarMoradaDTO dto)
    {
        try
        {
            var id = ObterMembroIdDoToken();
            return Ok(_service.AtualizarMorada(id, dto));
        }
        catch (ArgumentException ex) { return BadRequest(ex.Message); }
    }

    // PUT api/membro/pagamento — RU07 (prep)
    [HttpPut("pagamento")]
    public ActionResult<MembroPerfilDTO> AtualizarMetodoPagamento(AtualizarMetodoPagamentoDTO dto)
    {
        try
        {
            var id = ObterMembroIdDoToken();
            return Ok(_service.AtualizarMetodoPagamento(id, dto));
        }
        catch (ArgumentException ex) { return BadRequest(ex.Message); }
    }

    // POST api/membro/logout — RU05
    [HttpPost("logout")]
    public IActionResult Logout()
    {
        try
        {
            var id = ObterMembroIdDoToken();
            _service.RealizarLogout(id);
            return NoContent(); // 204
        }
        catch (ArgumentException ex) { return BadRequest(ex.Message); }
    }
}
