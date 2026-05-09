using System.ComponentModel.DataAnnotations;

namespace ProjetoES.API.Models
{
    // A classe é abstracta porque ninguém é "apenas" um Utilizador solto. 
    // Ou é Membro/Cliente, ou é Administrador.
    public abstract class Utilizador
    {

        public int Id { get; set; }

        public string PrimeiroNome { get; set; } = string.Empty;

        public string UltimoNome { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public TipoUtilizador Tipo { get; set; } = TipoUtilizador.Membro;
    }
}