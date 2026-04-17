using System.ComponentModel.DataAnnotations;

namespace ProjetoES.Models
{
    // A classe é abstracta porque ninguém é "apenas" um Utilizador solto. 
    // Ou é Membro/Cliente, ou é Administrador.
    public abstract class Utilizador
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string PrimeiroNome { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string UltimoNome { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        public TipoUtilizador Tipo { get; set; } = TipoUtilizador.Membro;
    }
}