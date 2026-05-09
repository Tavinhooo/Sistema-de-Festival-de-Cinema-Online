using System.ComponentModel.DataAnnotations;

namespace ProjetoES.API.Models
{
    public class PasswordResetToken
    {
        public int Id { get; set; }

        public string Email { get; set; } = string.Empty;

        public string Token { get; set; } = string.Empty;

        public DateTime DataExpiracao { get; set; }
    }
}
