namespace ProjetoES.API.DTOs;


/// <summary>
/// DTO para a sessão de checkout do Stripe, incluindo o ID da sessão e a chave publicável.
/// </summary>
public class StripeCheckoutSessionDTO
{
    public string SessionId { get; set; } = string.Empty;

    public string PublishableKey { get; set; } = string.Empty;

    public string CheckoutUrl { get; set; } = string.Empty;
}
