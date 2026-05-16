using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoES.API.DTOS;
using ProjetoES.API.Services;

namespace ProjetoES.API.Controllers;

[ApiController]
[Route("api/carrinhos")]
public class CarrinhosController : ControllerBase
{
    private readonly CarrinhoService _service;

    public CarrinhosController(CarrinhoService service)
    {
        _service = service;
    }

    [HttpGet]
    [Authorize]
    public ActionResult<List<CarrinhoResponseDTO>> ObterTodosCarrinhos()
    {
        return Ok(_service.ObterTodosCarrinhos());
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public ActionResult<CarrinhoResponseDTO> ObterCarrinhoPorId(int id)
    {
        var carrinho = _service.ObterCarrinhoPorId(id);
        if (carrinho == null)
        {
            return NotFound();
        }

        return Ok(carrinho);
    }

    [HttpGet("usuario/{utilizadorId}")]
    [AllowAnonymous]
    public ActionResult<CarrinhoResponseDTO> ObterCarrinhoPorUtilizador(int utilizadorId)
    {
        var carrinho = _service.ObterCarrinhoPorUtilizador(utilizadorId);
        if (carrinho == null)
        {
            return NotFound();
        }

        return Ok(carrinho);
    }

    [HttpPost]
    [AllowAnonymous]
    public ActionResult<CarrinhoResponseDTO> CriarCarrinho(CarrinhoRequestDTO dto)
    {
        try
        {
            var resultado = _service.CriarCarrinho(dto);
            return CreatedAtAction(nameof(ObterCarrinhoPorId), new { id = resultado.Id }, resultado);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{carrinhoId}/itens")]
    [AllowAnonymous]
    public ActionResult<CarrinhoResponseDTO> AdicionarItem(int carrinhoId, ItemCarrinhoRequestDTO dto)
    {
        try
        {
            return Ok(_service.AdicionarItem(carrinhoId, dto));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{carrinhoId}/itens/{itemId}")]
    [AllowAnonymous]
    public ActionResult<CarrinhoResponseDTO> RemoverItem(int carrinhoId, int itemId)
    {
        try
        {
            return Ok(_service.RemoverItem(carrinhoId, itemId));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{carrinhoId}/itens/{itemId}")]
    [AllowAnonymous]
    public ActionResult<CarrinhoResponseDTO> AtualizarQuantidade(int carrinhoId, int itemId, [FromBody] AtualizarItemDTO dto)
    {
        try
        {
            return Ok(_service.AtualizarQuantidade(carrinhoId, itemId, dto.Quantidade));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    [AllowAnonymous]
    public ActionResult RemoverCarrinho(int id)
    {
        var carrinho = _service.ObterCarrinhoPorId(id);
        if (carrinho == null)
        {
            return NotFound();
        }

        _service.RemoverCarrinho(id);
        return NoContent();
    }
}