using Microsoft.AspNetCore.Mvc;
using ProjetoES.API.Interfaces;
using ProjetoES.API.Models;
using ProjetoES.API.DTOs;

namespace ProjetoES.API.Controllers
{
    [ApiController]
    [Route("api/admin")]
    public class AdministradorController : ControllerBase
    {
        private readonly IAdministradorService _service;
        public AdministradorController(IAdministradorService service) => _service = service;

        // ── Filmes ──────────────────────────────────────────
        [HttpPost("{adminId}/filmes")]
        public async Task<IActionResult> GerirFilme(int adminId, [FromBody] FilmeDTO dto)
        {
            try { return Ok(await _service.GerirFilme(adminId, dto)); }
            catch (UnauthorizedAccessException ex) { return Forbid(ex.Message); }
            catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
        }

        [HttpDelete("{adminId}/filmes/{filmeId}")]
        public async Task<IActionResult> EliminarFilme(int adminId, int filmeId)
        {
            try { await _service.EliminarFilme(adminId, filmeId); return NoContent(); }
            catch (UnauthorizedAccessException ex) { return Forbid(ex.Message); }
        }

        // ── Festivais ───────────────────────────────────────
        [HttpPost("{adminId}/festivais")]
        public async Task<IActionResult> GerirFestival(int adminId, [FromBody] FestivalDTO dto)
        {
            try { return Ok(await _service.GerirFestival(adminId, dto)); }
            catch (UnauthorizedAccessException ex) { return Forbid(ex.Message); }
            catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
            catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
        }

        [HttpDelete("{adminId}/festivais/{festivalId}")]
        public async Task<IActionResult> EliminarFestival(int adminId, int festivalId)
        {
            try { await _service.EliminarFestival(adminId, festivalId); return NoContent(); }
            catch (UnauthorizedAccessException ex) { return Forbid(ex.Message); }
            catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
        }

        // ── Sessões ─────────────────────────────────────────
        [HttpPost("{adminId}/sessoes")]
        public async Task<IActionResult> CriarSessao(int adminId, [FromBody] SessaoDTO dto)
        {
            try { return Ok(await _service.GerirSessao(adminId, dto)); }
            catch (UnauthorizedAccessException ex) { return Forbid(ex.Message); }
        }

        [HttpPut("{adminId}/sessoes")]
        public async Task<IActionResult> AtualizarSessao(int adminId, [FromBody] SessaoDTO dto)
        {
            try { await _service.AtualizarSessao(adminId, dto); return NoContent(); }
            catch (UnauthorizedAccessException ex) { return Forbid(ex.Message); }
            catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
            catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
        }

        [HttpDelete("{adminId}/sessoes/{sessaoId}")]
        public async Task<IActionResult> CancelarSessao(int adminId, int sessaoId)
        {
            try { await _service.CancelarSessao(adminId, sessaoId); return NoContent(); }
            catch (UnauthorizedAccessException ex) { return Forbid(ex.Message); }
            catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
        }

        // ── Utilizadores ────────────────────────────────────
        [HttpGet("{adminId}/utilizadores")]
        public async Task<IActionResult> ListarUtilizadores(int adminId)
        {
            try { return Ok(await _service.ListarUtilizadores(adminId)); }
            catch (UnauthorizedAccessException ex) { return Forbid(ex.Message); }
        }

        [HttpPatch("{adminId}/utilizadores/{utilizadorId}/tipo")]
        public async Task<IActionResult> AlterarTipoUtilizador(int adminId, int utilizadorId, [FromBody] AlterarTipoDTO dto)
        {
            try { return Ok(await _service.AlterarTipoUtilizador(adminId, utilizadorId, dto.NovoTipo)); }
            catch (UnauthorizedAccessException ex) { return Forbid(ex.Message); }
            catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
        }

        [HttpDelete("{adminId}/utilizadores/{utilizadorId}")]
        public async Task<IActionResult> EliminarUtilizador(int adminId, int utilizadorId)
        {
            try { await _service.EliminarUtilizador(adminId, utilizadorId); return NoContent(); }
            catch (UnauthorizedAccessException ex) { return Forbid(ex.Message); }
        }

        // ── Avaliações ──────────────────────────────────────
        [HttpGet("{adminId}/avaliacoes")]
        public async Task<IActionResult> ListarAvaliacoes(int adminId)
        {
            try { return Ok(await _service.ListarAvaliacoes(adminId)); }
            catch (UnauthorizedAccessException ex) { return Forbid(ex.Message); }
        }

        [HttpPatch("{adminId}/avaliacoes/{avaliacaoId}/aprovar")]
        public async Task<IActionResult> AprovarAvaliacao(int adminId, int avaliacaoId)
        {
            try { return Ok(await _service.AprovarAvaliacao(adminId, avaliacaoId)); }
            catch (UnauthorizedAccessException ex) { return Forbid(ex.Message); }
            catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
        }

        [HttpDelete("{adminId}/avaliacoes/{avaliacaoId}")]
        public async Task<IActionResult> EliminarAvaliacao(int adminId, int avaliacaoId)
        {
            try { await _service.EliminarAvaliacao(adminId, avaliacaoId); return NoContent(); }
            catch (UnauthorizedAccessException ex) { return Forbid(ex.Message); }
        }

        // ── Histórico ───────────────────────────────────────
        [HttpGet("{adminId}/historico")]
        public async Task<IActionResult> ConsultarHistoricoGeral(int adminId, [FromQuery] DateTime? de, [FromQuery] DateTime? ate)
        {
            try { return Ok(await _service.ConsultarHistoricoGeral(adminId, de, ate)); }
            catch (UnauthorizedAccessException ex) { return Forbid(ex.Message); }
        }

        [HttpGet("{adminId}/historico/{utilizadorId}")]
        public async Task<IActionResult> ConsultarHistoricoPorUtilizador(int adminId, int utilizadorId, [FromQuery] DateTime? de, [FromQuery] DateTime? ate)
        {
            try { return Ok(await _service.ConsultarHistoricoPorUtilizador(adminId, utilizadorId, de, ate)); }
            catch (UnauthorizedAccessException ex) { return Forbid(ex.Message); }
        }
    }
}