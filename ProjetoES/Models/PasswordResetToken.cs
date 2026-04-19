using System.ComponentModel.DataAnnotations;

namespace ProjetoES.Models
{
    public class PasswordResetToken
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Token { get; set; } = string.Empty;

        public DateTime DataExpiracao { get; set; }
    }
}
