using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoES.API.DTOs;
using ProjetoES.API.Services;

namespace ProjetoES.API.Controllers;

[ApiController]
[Route("api/membro")]
[Authorize(Roles = "Membro,Cliente,Administrador")] 
/// <summary>
/// Controlador para gerir as funcionalidades específicas dos Membros, incluindo visualização e atualização do perfil, gestão de morada e método de pagamento, e logout.
/// Inclui métodos para obter o perfil (RU11), atualizar o perfil (RU11), atualizar morada (RU12), atualizar método de pagamento (RU12) e realizar logout (RU14).
/// </summary>
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

    // PUT api/membro/morada 
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

    // PUT api/membro/pagamento
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

    // POST api/membro/logout 
    [HttpPost("logout")]
    public IActionResult Logout()
    {
        try
        {
            var id = ObterMembroIdDoToken();
            _service.RealizarLogout(id);
            return NoContent(); 
        }
        catch (ArgumentException ex) { return BadRequest(ex.Message); }
    }
}
