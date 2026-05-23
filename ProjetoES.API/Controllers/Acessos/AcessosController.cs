using Microsoft.AspNetCore.Mvc;
using ProjetoES.API.Repositories;
using ProjetoES.API.Models;

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

    [HttpGet("utilizador/{userId}/festival/{festivalId}/filme/{filmeId}/verificar")]
    public ActionResult<bool> VerificarAcessoFestival(int userId,int filmeId ,int festivalId)
    {
        var temAcesso = _repository.VerificarAcessoFilmeNoFestival(userId, filmeId, festivalId);
        return Ok(temAcesso);
    }

    [HttpGet("utilizador/{userId}/filmes")]
    public ActionResult<List<Filme>> ObterFilmesComAcesso(int userId)
    {
        return Ok(_repository.ObterFilmesComAcesso(userId));
    }
}