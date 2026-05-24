using System.ComponentModel.DataAnnotations;

namespace ProjetoES.API.Models
{
    /// <summary>
    /// Modelo de token de redefinição de password, representando um token gerado para um utilizador que solicitou
    ///  a redefinição da sua password, incluindo o email do utilizador, o token gerado e a data de expiração do token.
    ///  Este modelo é utilizado
    /// </summary>
    public class PasswordResetToken
    {
        public int Id { get; set; }

        public string Email { get; set; } = string.Empty;

        public string Token { get; set; } = string.Empty;

        public DateTime DataExpiracao { get; set; }
    }
}
