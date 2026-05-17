using Microsoft.AspNetCore.Mvc;
using ProjetoES.API.Repositories;

namespace ProjetoES.API.Controllers;

[ApiController]
[Route("api/acessos")]
public class AcessosController : ControllerBase
{
    private readonly AcessoRepository _repository;

    public AcessosController(AcessoRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("utilizador/{userId}/festival/{festivalId}")]
    public ActionResult<bool> VerificarAcessoFestival(int userId, int festivalId)
    {
        var temAcesso = _repository.TemAcessoAFestival(userId, festivalId);
        return Ok(temAcesso);
    }

    [HttpGet("utilizador/{userId}/filmes")]
    public ActionResult<List<int>> ObterFilmesComAcesso(int userId)
    {
        return Ok(_repository.ObterFilmesComAcesso(userId));
    }
}