using ProjetoES.API.DTOs;
using ProjetoES.API.Models;
using ProjetoES.API.Repositories;
using ProjetoES.API.Pricing;

namespace ProjetoES.API.Services
{
    public class CheckoutService
    {
        private readonly CarrinhoRepository _carrinhoRepo;
        private readonly PedidoRepository _pedidoRepo;
        private readonly AcessoRepository _acessoRepo;
        private readonly FilmeRepository _filmeRepo;

        public CheckoutService(CarrinhoRepository carrinhoRepo, PedidoRepository pedidoRepo, AcessoRepository acessoRepo, FilmeRepository filmeRepo)
        {
            _carrinhoRepo = carrinhoRepo;
            _pedidoRepo = pedidoRepo;
            _acessoRepo = acessoRepo;
            _filmeRepo = filmeRepo;
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
                Console.WriteLine($"Item: FilmeId={itemCarrinho.FilmeId}, FestivalId={itemCarrinho.FestivalId}, Tipo={itemCarrinho.TipoAcesso}");

                var tipoNormalizado = string.IsNullOrWhiteSpace(itemCarrinho.TipoAcesso)
                    ? string.Empty
                    : RemoverAcentos(itemCarrinho.TipoAcesso.Trim().ToLowerInvariant());
                if (tipoNormalizado == "passe completo" || tipoNormalizado == "passe diario")
                {
                    var filmesFestival = _filmeRepo.ObterFilmesPorFestival(itemCarrinho.FestivalId);
                    foreach (var filme in filmesFestival)
                    {
                        for (int i = 0; i < itemCarrinho.Quantidade; i++)
                        {
                            acessos.Add(CriarAcesso(memberId, filme.Id, itemCarrinho.FestivalId, itemCarrinho.TipoAcesso));
                        }
                    }
                    continue;
                }

                if (!itemCarrinho.FilmeId.HasValue)
                {
                    throw new ArgumentException("O filme é obrigatório para este tipo de acesso.");
                }

                for (int i = 0; i < itemCarrinho.Quantidade; i++)
                {
                    acessos.Add(CriarAcesso(memberId, itemCarrinho.FilmeId.Value, itemCarrinho.FestivalId, itemCarrinho.TipoAcesso));
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
                    FilmeId = i.FilmeId ?? 0,
                    FilmeTitulo = i.Filme?.Titulo ?? string.Empty,
                    Quantidade = i.Quantidade,
                    PrecoUnitario = i.PrecoUnitario
                }).ToList()
            };
        }

        private static Acesso CriarAcesso(int memberId, int filmeId, int festivalId, string tipoAcesso)
        {
            return new Acesso
            {
                ClienteId = memberId,
                FilmeId = filmeId,
                FestivalId = festivalId,
                DataAquisicao = DateTime.UtcNow,
                DataValidade = DateTime.UtcNow.AddDays(30),
                Estado = EstadoAcesso.Ativo,
                TipoAcesso = tipoAcesso
            };
        }

        private static string RemoverAcentos(string texto)
        {
            var textoNormalizado = texto.Normalize(System.Text.NormalizationForm.FormD);
            var builder = new System.Text.StringBuilder(texto.Length);

            foreach (var caractere in textoNormalizado)
            {
                var categoria = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(caractere);
                if (categoria != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    builder.Append(caractere);
                }
            }

            return builder.ToString().Normalize(System.Text.NormalizationForm.FormC);
        }
    }
}
