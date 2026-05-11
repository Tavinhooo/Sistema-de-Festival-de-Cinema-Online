using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoES.API.DTOs;
using ProjetoES.API.Services;
using ProjetoES.API.Repositories;
using System.Security.Claims;

namespace ProjetoES.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CheckoutController : ControllerBase
    {
        private readonly CheckoutService _checkoutService;
        private readonly PedidoRepository _pedidoRepository;

        public CheckoutController(CheckoutService checkoutService, PedidoRepository pedidoRepository)
        {
            _checkoutService = checkoutService;
            _pedidoRepository = pedidoRepository;
        }

        // POST: api/checkout
        [HttpPost]
        public IActionResult Checkout([FromBody] CheckoutRequestDTO dto)
        {
            try
            {
                var memberId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
                if (memberId == 0)
                    return Unauthorized("Utilizador não autenticado.");

                var pedido = _checkoutService.Checkout(dto.CarrinhoId, memberId, dto.MetodoPagamento);
                return Ok(pedido);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/checkout/historico
        [HttpGet("historico")]
        public IActionResult ObterHistorico()
        {
            try
            {
                var memberId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
                if (memberId == 0)
                    return Unauthorized("Utilizador não autenticado.");

                var pedidos = _pedidoRepository.ObterPedidosPorMembro(memberId);
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
                var memberId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
                var pedido = _pedidoRepository.ObterPedidoPorId(id);

                if (pedido == null)
                    return NotFound("Pedido não encontrado.");

                // Verificar que o pedido pertence ao utilizador autenticado
                if (pedido.MemberId != memberId)
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
