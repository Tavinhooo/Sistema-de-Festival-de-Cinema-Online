using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoES.API.Services.Premios;

namespace ProjetoES.API.Controllers;

[ApiController]
[Route("api/premios")]
public class PremiosController : ControllerBase
{
    private readonly PremioService _service;

    public PremiosController(PremioService service)
    {
        _service = service;
    }

    private int ObterClienteId() =>
        int.Parse(User.FindFirstValue("sub")
            ?? User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new UnauthorizedAccessException("Token inválido."));

    // ── GET /api/premios/festival/{festivalId} ───────────────────────────
    [HttpGet("festival/{festivalId:int}")]
    public ActionResult ObterPorFestival(int festivalId)
    {
        var premios = _service.ObterPremiosPorFestival(festivalId);
        var userId = ApiClient_getCurrentUserId();

        var response = premios.Select(p => new
        {
            p.Id,
            p.Nome,
            p.Descricao,
            p.FestivalId,
            DataLimiteVotacao = p.DataLimiteVotacao,
            VotacaoAberta = p.DataLimiteVotacao == null || p.DataLimiteVotacao > DateTime.UtcNow,
            TotalVotos = p.Votos.Count,
            MeuVotoFilmeId = userId.HasValue
                ? _service.ObterVotoUtilizador(p.Id, userId.Value)?.FilmeId
                : null
        });

        return Ok(response);
    }

    // ── GET /api/premios/{id}/resultados ────────────────────────────────
    [HttpGet("{id:int}/resultados")]
    public ActionResult ObterResultados(int id)
    {
        try
        {
            return Ok(_service.ObterResultados(id));
        }
        catch (ArgumentException ex) { return NotFound(ex.Message); }
    }

    // ── POST /api/premios/{id}/votar ────────────────────────────────────
    [HttpPost("{id:int}/votar")]
    [Authorize(Roles = "Cliente,Administrador")]
    public ActionResult Votar(int id, [FromBody] VotarDTO dto)
    {
        try
        {
            var clienteId = ObterClienteId();
            var voto = _service.Votar(id, clienteId, dto.FilmeId);
            return Ok(new { voto.Id, voto.PremioId, voto.FilmeId, voto.DataVoto });
        }
        catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
        catch (ArgumentException ex) { return BadRequest(ex.Message); }
        catch (UnauthorizedAccessException) { return Forbid(); }
    }

    // ── POST /api/premios ── (Admin only) ───────────────────────────────
    [HttpPost]
    [Authorize(Roles = "Administrador")]
    public ActionResult CriarPremio([FromBody] CriarPremioDTO dto)
    {
        try
        {
            var premio = _service.CriarPremio(dto.FestivalId, dto.Nome, dto.Descricao, dto.DataLimiteVotacao);
            return CreatedAtAction(nameof(ObterResultados), new { id = premio.Id }, new { premio.Id, premio.Nome });
        }
        catch (ArgumentException ex) { return BadRequest(ex.Message); }
    }

    // ── DELETE /api/premios/{id} ── (Admin only) ─────────────────────────
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Administrador")]
    public ActionResult EliminarPremio(int id)
    {
        try
        {
            _service.EliminarPremio(id);
            return NoContent();
        }
        catch (ArgumentException ex) { return NotFound(ex.Message); }
    }

    // ── PATCH /api/premios/{id}/votacao ── (Admin only) ──────────────────
    [HttpPatch("{id:int}/votacao")]
    [Authorize(Roles = "Administrador")]
    public ActionResult ToggleVotacao(int id, [FromBody] bool aberta)
    {
        try
        {
            _service.ToggleVotacao(id, aberta);
            return NoContent();
        }
        catch (ArgumentException ex) { return NotFound(ex.Message); }
    }

    // ── Helper ───────────────────────────────────────────────────────────
    private int? ApiClient_getCurrentUserId()
    {
        var sub = User.FindFirstValue("sub") ?? User.FindFirstValue(ClaimTypes.NameIdentifier);
        return int.TryParse(sub, out var id) ? id : null;
    }
}

// ── DTOs ─────────────────────────────────────────────────────────────────────
public class VotarDTO
{
    public int FilmeId { get; set; }
}

public class CriarPremioDTO
{
    public int FestivalId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public DateTime? DataLimiteVotacao { get; set; }
}