using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoES.API.DTOs;
using ProjetoES.API.Services;
using ProjetoES.API.Repositories;
using System.Security.Claims;

namespace ProjetoES.API.Controllers
{
    [ApiController]
    [Route("api/checkout")]
    [Authorize]
    public class CheckoutController : ControllerBase
    {
        private readonly CheckoutFacade _checkoutFacade;
        private readonly PedidoRepository _pedidoRepository;

        public CheckoutController(CheckoutFacade checkoutFacade, PedidoRepository pedidoRepository)
        {
            _checkoutFacade = checkoutFacade;
            _pedidoRepository = pedidoRepository;
        }

        private int ObterUtilizadorIdDoToken()
        {
            var sub = User.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? throw new UnauthorizedAccessException("Token inválido.");
            return int.Parse(sub);
        }

        // POST: api/checkout — RF04: processa compra e promove Membro para Cliente
        [HttpPost]
        public IActionResult ProcessarCheckout(CheckoutRequestDTO dto)
        {
            try
            {
                var utilizadorId = ObterUtilizadorIdDoToken();
                var resultado = _checkoutFacade.ProcessarCheckout(utilizadorId, dto.MetodoPagamento);
                return Created(string.Empty, resultado);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET: api/checkout/historico
        [HttpGet("historico")]
        public IActionResult ObterHistorico()
        {
            try
            {
                var utilizadorId = ObterUtilizadorIdDoToken();
                var pedidos = _pedidoRepository.ObterPedidosPorMembro(utilizadorId);
                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET: api/checkout/{id}
        [HttpGet("{id}")]
        public IActionResult ObterPedido(int id)
        {
            try
            {
                var utilizadorId = ObterUtilizadorIdDoToken();
                var pedido = _pedidoRepository.ObterPedidoPorId(id);

                if (pedido == null)
                    return NotFound("Pedido não encontrado.");

                if (pedido.UtilizadorId != utilizadorId)
                    return Forbid("Não tem permissão para ver este pedido.");

                return Ok(pedido);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}