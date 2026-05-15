using Microsoft.AspNetCore.Mvc;
using ProjetoES.API.DTOS;
using ProjetoES.API.Repositories;

namespace ProjetoES.API.Controllers;

[ApiController]
[Route("api/avaliacoes")]
public class AvaliacoesController : ControllerBase
{
    private readonly AvaliacaoRepository _repository;

    public AvaliacoesController(AvaliacaoRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("filme/{filmeId}")]
    public ActionResult<List<AvaliacaoResponseDTO>> ObterAvaliacoesPorFilme(int filmeId)
    {
        var avaliacoes = _repository.ObterAvaliacoesPorFilme(filmeId);

        var result = avaliacoes.Select(a => new AvaliacaoResponseDTO
        {
            Id = a.Id,
            FilmeId = a.FilmeId,
            Classificacao = a.Classificacao,
            Comentario = a.Comentario,
            DataAvaliacao = a.DataAvaliacao,
            ClienteNome = a.Cliente != null ? $"{a.Cliente.PrimeiroNome} {a.Cliente.UltimoNome}" : "Utilizador"
        }).ToList();

        return Ok(result);
    }
}