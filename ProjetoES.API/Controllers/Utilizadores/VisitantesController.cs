using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoES.API.DTOs;
using ProjetoES.API.Repositories;

namespace ProjetoES.API.Controllers;

[ApiController]
[Route("api/visitantes")]
public class VisitantesController : ControllerBase
{
    private readonly AuthRepository _authRepository;

    public VisitantesController(AuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    // Cria uma sessão de visitante anónimo para carrinho e navegação pública.
    [HttpPost("sessao")]
    [AllowAnonymous]
    public ActionResult<VisitanteSessionDTO> CriarSessaoVisitante()
    {
        var visitante = _authRepository.CriarVisitante();
        return Created(string.Empty, new VisitanteSessionDTO
        {
            VisitanteId = visitante.Id,
            IsLogged = visitante.IsLogged
        });
    }
}
