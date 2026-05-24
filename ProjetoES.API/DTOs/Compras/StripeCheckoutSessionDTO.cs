namespace ProjetoES.API.DTOs;

public class StripeCheckoutSessionDTO
{
    public string SessionId { get; set; } = string.Empty;

    public string PublishableKey { get; set; } = string.Empty;

    public string CheckoutUrl { get; set; } = string.Empty;
}
