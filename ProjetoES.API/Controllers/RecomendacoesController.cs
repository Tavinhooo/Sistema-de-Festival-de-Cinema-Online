using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoES.API.Services.Recomendacoes;

namespace ProjetoES.API.Controllers;

[ApiController]
[Route("api/recomendacoes")]
public class RecomendacoesController : ControllerBase
{
    private readonly RecomendacoesService _service;

    public RecomendacoesController(RecomendacoesService service)
    {
        _service = service;
    }

    /// <summary>
    /// GET api/recomendacoes/{userId}
    /// Devolve recomendações agrupadas por estratégia (género, popularidade, festival).
    /// </summary>
    [HttpGet("{userId:int}")]
    public ActionResult ObterRecomendacoes(int userId)
    {
        try
        {
            var recomendacoes = _service.ObterRecomendacoes(userId);

            // Mapeia para um formato limpo para o frontend
            var response = recomendacoes
                .Where(kvp => kvp.Value.Any())
                .Select(kvp => new
                {
                    estrategia = kvp.Key,
                    filmes = kvp.Value.Select(f => new
                    {
                        f.Id,
                        f.Titulo,
                        f.Genero,
                        f.Ano,
                        f.MediaAvaliacao,
                        f.PosterUrl,
                        f.DuracaoMinutos,
                        festivaisIds = f.Festivais.Select(fv => fv.Id).ToList()
                    })
                });

            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// GET api/recomendacoes/{userId}/estrategia?nome=Por Género
    /// Devolve recomendações de uma estratégia específica.
    /// </summary>
    [HttpGet("{userId:int}/estrategia")]
    public ActionResult ObterPorEstrategia(int userId, [FromQuery] string nome)
    {
        try
        {
            var filmes = _service.ObterRecomendacoesPorEstrategia(userId, nome)
                .Select(f => new
                {
                    f.Id,
                    f.Titulo,
                    f.Genero,
                    f.Ano,
                    f.MediaAvaliacao,
                    f.PosterUrl,
                    f.DuracaoMinutos,
                    festivaisIds = f.Festivais.Select(fv => fv.Id).ToList()
                });

            return Ok(filmes);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}