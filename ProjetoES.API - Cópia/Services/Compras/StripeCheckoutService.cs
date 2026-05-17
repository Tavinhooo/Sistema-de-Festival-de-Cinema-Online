using ProjetoES.API.DTOs;
using ProjetoES.API.Repositories;
using Stripe;
using Stripe.Checkout;

namespace ProjetoES.API.Services;

public class StripeCheckoutService
{
    private readonly CarrinhoRepository _carrinhoRepository;
    private readonly IConfiguration _configuration;

    public StripeCheckoutService(CarrinhoRepository carrinhoRepository, IConfiguration configuration)
    {
        _carrinhoRepository = carrinhoRepository;
        _configuration = configuration;
    }

    public StripeCheckoutSessionDTO CriarSessao(int utilizadorId)
    {
        var carrinho = _carrinhoRepository.ObterComItens(utilizadorId)
            ?? throw new InvalidOperationException("Carrinho não encontrado.");

        if (carrinho.Itens == null || !carrinho.Itens.Any())
            throw new InvalidOperationException("O carrinho está vazio.");

        var secretKey = _configuration["StripeSettings:SecretKey"];
        if (string.IsNullOrWhiteSpace(secretKey))
            throw new InvalidOperationException("A chave secreta da Stripe não está configurada.");

        var publishableKey = _configuration["StripeSettings:PublishableKey"] ?? string.Empty;
        var successUrl = _configuration["StripeSettings:SuccessUrl"]
            ?? throw new InvalidOperationException("A URL de sucesso da Stripe não está configurada.");
        var cancelUrl = _configuration["StripeSettings:CancelUrl"]
            ?? throw new InvalidOperationException("A URL de cancelamento da Stripe não está configurada.");

        StripeConfiguration.ApiKey = secretKey;

        var options = new SessionCreateOptions
        {
            Mode = "payment",
            SuccessUrl = successUrl,
            CancelUrl = cancelUrl,
            ClientReferenceId = utilizadorId.ToString(),
            Metadata = new Dictionary<string, string>
            {
                ["utilizadorId"] = utilizadorId.ToString(),
                ["carrinhoId"] = carrinho.Id.ToString()
            },
            LineItems = carrinho.Itens
                .Where(item => item.Quantidade > 0)
                .Select(item => new SessionLineItemOptions
                {
                    Quantity = item.Quantidade,
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "eur",
                        UnitAmount = (long)Math.Round((decimal)item.PrecoUnitario * 100m),
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Filme?.Titulo ?? $"Filme {item.FilmeId}",
                            Description = item.TipoAcesso
                        }
                    }
                })
                .ToList()
        };

        var service = new SessionService();
        var session = service.Create(options);

        return new StripeCheckoutSessionDTO
        {
            SessionId = session.Id,
            PublishableKey = publishableKey,
            CheckoutUrl = session.Url ?? string.Empty
        };
    }
}
