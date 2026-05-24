using Microsoft.AspNetCore.Mvc;
using ProjetoES.API.DTOs;
using ProjetoES.API.DTOS;
using ProjetoES.API.Repositories;

namespace ProjetoES.API.Controllers;

[ApiController]
[Route("api/avaliacoes")]
/// <summary>
    /// Controlador para gerir as avaliações dos filmes pelos clientes, incluindo funcionalidades de reporte e moderação.
/// </summary>
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
            ClienteId = a.ClienteId,
            Classificacao = a.Classificacao,
            Comentario = a.Comentario,
            DataAvaliacao = a.DataAvaliacao,
            MotivoReporte = a.MotivoReporte,
            ClienteNome = a.Cliente != null ? $"{a.Cliente.PrimeiroNome} {a.Cliente.UltimoNome}" : "Utilizador"
        }).ToList();

        return Ok(result);
    }

    [HttpPost("{id}/reportar")]
    public ActionResult ReportarAvaliacao(int id, [FromBody] ReportarAvaliacaoDTO dto)
    {
        var avaliacao = _repository.ObterAvaliacaoPorId(id);
        if (avaliacao == null) return NotFound();

        avaliacao.IsReportado = true;
        avaliacao.MotivoReporte = dto.Motivo;
        _repository.AtualizarAvaliacao(avaliacao);
        return NoContent();
    }

    [HttpPost("{id}/ignorar-report")]
    public ActionResult IgnorarReport(int id)
    {
        var avaliacao = _repository.ObterAvaliacaoPorId(id);
        if (avaliacao == null) return NotFound();
        avaliacao.IsReportado = false;
        _repository.AtualizarAvaliacao(avaliacao);
        return NoContent();
    }

}
