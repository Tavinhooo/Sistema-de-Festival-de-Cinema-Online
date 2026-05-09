namespace ProjetoES.API.Models
{
    public abstract class Visitante
    {
        public int Id { get; set; }
        public string PrimeiroNome { get; set; } = string.Empty;
        public string UltimoNome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public bool IsLogged { get; set; }
    }
}