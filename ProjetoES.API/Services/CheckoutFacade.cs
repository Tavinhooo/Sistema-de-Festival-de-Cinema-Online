using ProjetoES.API.Models;
using ProjetoES.API.Repositories;

namespace ProjetoES.API.Services
{
    public class CheckoutFacade
    {
        private readonly CarrinhoRepository _carrinhoRepo;
        private readonly PedidoRepository _pedidoRepo;
        private readonly AcessoService _acessoService;

        public CheckoutFacade(
            CarrinhoRepository carrinhoRepo,
            PedidoRepository pedidoRepo,
            AcessoService acessoService)
        {
            _carrinhoRepo = carrinhoRepo;
            _pedidoRepo = pedidoRepo;
            _acessoService = acessoService;
        }

        public Pedido ProcessarCheckout(int utilizadorId, string metodoPagamento)
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
                MemberId = utilizadorId,
                DataPedido = DateTime.UtcNow,
                DataPagamento = DateTime.UtcNow,
                Total = (double)itensPedido.Sum(i => i.PrecoUnitario),
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

            return pedido;
        }
    }
}