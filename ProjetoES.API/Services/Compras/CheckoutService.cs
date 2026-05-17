using ProjetoES.API.DTOs;
using ProjetoES.API.Models;
using ProjetoES.API.Repositories;

namespace ProjetoES.API.Services
{
    public class CheckoutService
    {
        private readonly CarrinhoRepository _carrinhoRepo;
        private readonly PedidoRepository _pedidoRepo;
        private readonly AcessoRepository _acessoRepo;

        public CheckoutService(CarrinhoRepository carrinhoRepo, PedidoRepository pedidoRepo, AcessoRepository acessoRepo)
        {
            _carrinhoRepo = carrinhoRepo;
            _pedidoRepo = pedidoRepo;
            _acessoRepo = acessoRepo;
        }

        public PedidoResponseDTO Checkout(int carrinhoId, int memberId, string metodoPagamento)
        {
            // Obter carrinho com itens
            var carrinho = _carrinhoRepo.ObterCarrinhoPorId(carrinhoId);
            if (carrinho == null)
                throw new ArgumentException("Carrinho não encontrado.");

            if (carrinho.Itens == null || carrinho.Itens.Count == 0)
                throw new ArgumentException("Carrinho vazio.");

            // Calcular PrecoTotal
            var PrecoTotal = carrinho.Itens.Sum(i => i.PrecoUnitario * i.Quantidade);

            // Criar pedido
            var pedido = new Pedido
            {
                UtilizadorId = memberId,
                DataPedido = DateTime.UtcNow,
                DataPagamento = DateTime.UtcNow,
                PrecoTotal = PrecoTotal,
                Estado = EstadoPedido.Completo
            };

            // Converter itens do carrinho para itens do pedido
            foreach (var itemCarrinho in carrinho.Itens)
            {
                pedido.Itens.Add(new ItemPedido
                {
                    FilmeId = itemCarrinho.FilmeId,
                    Quantidade = itemCarrinho.Quantidade,
                    PrecoUnitario = itemCarrinho.PrecoUnitario,
                    TipoAcesso = itemCarrinho.TipoAcesso,
                    Status = "Pedido"
                });
            }

            // Criar Acessos para cada quantidade de filme comprado
            var acessos = new List<Acesso>();
            foreach (var itemCarrinho in carrinho.Itens)
            {
                for (int i = 0; i < itemCarrinho.Quantidade; i++)
                {
                    var acesso = new Acesso
                    {
                        ClienteId = memberId,
                        FilmeId = itemCarrinho.FilmeId,
                        DataAquisicao = DateTime.UtcNow,
                        DataValidade = DateTime.UtcNow.AddDays(30), // Bilhete válido por 30 dias
                        Estado = EstadoAcesso.Ativo,
                        TipoAcesso = "Bilhete"
                    };
                    acessos.Add(acesso);
                }
            }

            // Persistir pedido (através do repository)
            _pedidoRepo.CriarPedido(pedido);
            
            // Persistir acessos (através do repository)
            if (acessos.Any())
            {
                _acessoRepo.CriarAcessos(acessos);
            }

            // Limpar carrinho
            _carrinhoRepo.RemoverCarrinho(carrinhoId);

            // Retornar DTO do pedido
            return MapearParaResponse(pedido);
        }

        private PedidoResponseDTO MapearParaResponse(Pedido pedido)
        {
            return new PedidoResponseDTO
            {
                Id = pedido.Id,
                MemberId = pedido.UtilizadorId,   // DTO mantém o nome MemberId para não quebrar o frontend
                DataPedido = pedido.DataPedido,
                DataPagamento = pedido.DataPagamento,
                PrecoTotal = pedido.PrecoTotal,
                Estado = pedido.Estado.ToString(),
                Itens = pedido.Itens.Select(i => new ItemPedidoResponseDTO
                {
                    Id = i.Id,
                    FilmeId = i.FilmeId,
                    FilmeTitulo = i.Filme?.Titulo ?? string.Empty,
                    Quantidade = i.Quantidade,
                    PrecoUnitario = i.PrecoUnitario
                }).ToList()
            };
        }
    }
}
