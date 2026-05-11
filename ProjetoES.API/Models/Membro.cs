namespace ProjetoES.API.Models
{
    public class Membro : Visitante
    {
        public string PrimeiroNome { get; set; } = string.Empty;
        public string UltimoNome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string? MetodoPagamento { get; set; }
        //public Carrinho? Carrinho { get; set; }
        //public Morada? Morada { get; set; }
    }
}