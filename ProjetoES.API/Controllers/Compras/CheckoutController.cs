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
        private readonly StripeCheckoutService _stripeCheckoutService;

        public CheckoutController(CheckoutFacade checkoutFacade, PedidoRepository pedidoRepository, StripeCheckoutService stripeCheckoutService)
        {
            _checkoutFacade = checkoutFacade;
            _pedidoRepository = pedidoRepository;
            _stripeCheckoutService = stripeCheckoutService;
        }

        private int ObterUtilizadorIdDoToken()
        {
            // Debug: ver todos os claims que chegam
            foreach (var claim in User.Claims)
                Console.WriteLine($"CLAIM: {claim.Type} = {claim.Value}");

            var sub = User.FindFirstValue("sub")
                ?? User.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? User.FindFirstValue("nameid")
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

        // POST: api/checkout/stripe/session
        [HttpPost("stripe/session")]
        public IActionResult CriarStripeSession()
        {
            try
            {
                var utilizadorId = ObterUtilizadorIdDoToken();
                var session = _stripeCheckoutService.CriarSessao(utilizadorId);
                return Ok(session);
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

                var resultado = pedidos.Select(p => new HistoricoComprasDTO
                {
                    PedidoId = p.Id,
                    DataPedido = p.DataPedido,
                    DataPagamento = p.DataPagamento,
                    PrecoTotal = p.PrecoTotal,
                    Estado = p.Estado.ToString(),
                    Itens = p.Itens.Select(i => new ItemPedidoDTO
                    {
                        FilmeId = i.FilmeId ?? 0,
                        FilmeTitulo = i.Filme?.Titulo ?? "Filme",
                        TipoAcesso = i.TipoAcesso,
                        Quantidade = i.Quantidade,
                        PrecoUnitario = i.PrecoUnitario
                    }).ToList()
                }).ToList();

                return Ok(resultado);
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
