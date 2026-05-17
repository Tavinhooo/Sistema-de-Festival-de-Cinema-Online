using Microsoft.AspNetCore.Mvc;
using ProjetoES.API.DTOs;
using ProjetoES.API.Services;

namespace ProjetoES.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _service;

    public AuthController(AuthService service)
    {
        _service = service;
    }

    [HttpPost("register")]
    public ActionResult<AuthResponseDTO> Register(AuthRegisterDTO dto)
    {
        try
        {
            var resp = _service.Register(dto);
            return Created(string.Empty, resp);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("login")]
    public ActionResult<AuthResponseDTO> Login(AuthLoginDTO dto)
    {
        try
        {
            var resp = _service.Login(dto);
            return Ok(resp);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

