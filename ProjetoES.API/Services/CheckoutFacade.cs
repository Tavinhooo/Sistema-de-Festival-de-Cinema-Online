using ProjetoES.API.DTOs;
using ProjetoES.API.DTOS;
using ProjetoES.API.Models;
using ProjetoES.API.Repositories;

namespace ProjetoES.API.Services
{
    public class CheckoutFacade
    {
        private readonly CarrinhoRepository _carrinhoRepo;
        private readonly PedidoRepository _pedidoRepo;
        private readonly AcessoService _acessoService;
        private readonly ClienteService _clienteService;

        public CheckoutFacade(
            CarrinhoRepository carrinhoRepo,
            PedidoRepository pedidoRepo,
            AcessoService acessoService,
            ClienteService clienteService)
        {
            _carrinhoRepo = carrinhoRepo;
            _pedidoRepo = pedidoRepo;
            _acessoService = acessoService;
            _clienteService = clienteService;
        }

        public CheckoutResultDTO ProcessarCheckout(int utilizadorId, string metodoPagamento)
        {
            var carrinho = _carrinhoRepo.ObterComItens(utilizadorId)
                ?? throw new InvalidOperationException("Carrinho vazio ou não encontrado.");

            if (!carrinho.Itens.Any())
                throw new InvalidOperationException("Carrinho não tem itens.");

            var itensPedido = carrinho.Itens.Select(i => new ItemPedido
            {
                FilmeId = i.FilmeId,
                Quantidade = i.Quantidade,
                TipoAcesso = i.TipoAcesso,
                PrecoUnitario = i.PrecoUnitario
            }).ToList();

            var pedido = new Pedido
            {
                UtilizadorId = utilizadorId,
                DataPedido = DateTime.UtcNow,
                DataPagamento = DateTime.UtcNow,
                PrecoTotal = (double)itensPedido.Sum(i => i.PrecoUnitario),
                Estado = EstadoPedido.Completo,
                Itens = itensPedido
            };

            _pedidoRepo.CriarPedido(pedido);

            foreach (var ip in itensPedido)
            {
                var quantidade = ip.Quantidade <= 0 ? 1 : ip.Quantidade;
                _acessoService.CriarAcessos(utilizadorId, ip.FilmeId, quantidade, ip.TipoAcesso);
            }

            _carrinhoRepo.Limpar(carrinho);

            // RF04 — promover Membro para Cliente automaticamente após pagamento
            var resultado = new CheckoutResultDTO
            {
                PedidoId = pedido.Id,
                PrecoTotal = pedido.PrecoTotal,
                Estado = pedido.Estado.ToString()
            };

            try
            {
                var authResponse = _clienteService.PromoverParaCliente(utilizadorId);
                // Utilizador era Membro, foi promovido — devolve novo token com Role = "Cliente"
                resultado.NovoToken = authResponse.Token;
                resultado.TokenExpiresAt = authResponse.ExpiresAt;
            }
            catch (ArgumentException)
            {
                // Utilizador já era Cliente ou Administrador — não faz nada, continua normalmente
            }

            return resultado;
        }
    }
}