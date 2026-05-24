using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ProjetoES.API.Models;
using ProjetoES.API.Services;
using ProjetoES.API.Interfaces;

namespace ProjetoES.API.Controllers
{
    [ApiController]
    [Route("api/listaspessoais")]
    [Authorize]
    public class ListaPessoalController : ControllerBase
    {
        private readonly IListaPessoalService _service;

        public ListaPessoalController(IListaPessoalService service)
        {
            _service = service;
        }
        [HttpGet("{id}")]
    public ActionResult<ListaPessoal> ObterLista(int id)
    {
        var lista = _service.ObterLista(id);
        if (lista == null)
            return NotFound();

        return Ok(lista);
    }

    [HttpGet("membro/{membroId}")]
    public ActionResult<IEnumerable<ListaPessoal>> ObterPorMembro(int membroId)
    {
        return Ok(_service.ObterPorMembro(membroId));
    }

    [HttpPost]
    public ActionResult<ListaPessoal> CriarLista(int membroId, TipoLista tipo)
    {
        try
        {
            var lista = _service.CriarLista(membroId, tipo);
            return CreatedAtAction(nameof(ObterLista), new { id = lista.Id }, lista);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
     [HttpPost("{listaId}/filmes/{filmeId}")]
    public ActionResult AdicionarFilme(int listaId, int filmeId)
    {
        try
        {
            _service.AdicionarFilme(listaId, filmeId);
            return Ok();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{listaId}/filmes/{filmeId}")]
    public ActionResult RemoverFilme(int listaId, int filmeId)
    {
        try
        {
            _service.RemoverFilme(listaId, filmeId);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPut("{listaId}/tipo")]
    public ActionResult MudarTipo(int listaId, TipoLista novoTipo)
    {
        try
        {
            _service.MudarTipoLista(listaId, novoTipo);
            return Ok();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public ActionResult RemoverLista(int id)
    {
        var lista = _service.ObterLista(id);
        if (lista == null)
            return NotFound();

        return NoContent();
    }
    }
}