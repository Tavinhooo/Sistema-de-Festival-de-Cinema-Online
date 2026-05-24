using System.Collections.Generic;

namespace ProjetoES.API.Models
{
    /// <summary>
    /// Modelo de utilizador, representando um utilizador do sistema, que pode ser um membro, cliente ou administrador, dependendo do tipo de utilizador definido no campo TipoUtilizador. O modelo de utilizador inclui informações básicas como email, password hash, nome e sobrenome, além de informações específicas para cada tipo de utilizador, como data da primeira compra para clientes e data de promoção para administradores. A classe Utilizador é a classe base para os diferentes tipos de utilizadores no sistema, e a distinção entre os tipos é feita através do campo TipoUtilizador.
    /// </summary>
    public abstract class UtilizadorBase
    {
        public int Id { get; set; }
        public bool IsLogged { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string PrimeiroNome { get; set; } = string.Empty;
        public string UltimoNome { get; set; } = string.Empty;
        public string? MetodoPagamento { get; set; }
    }

    public class Utilizador : UtilizadorBase
    {
        public TipoUtilizador Tipo { get; set; } = TipoUtilizador.Membro;

        // RU06 - Morada de faturação
        public Morada? MoradaFaturacao { get; set; }

        // Dados específicos de Cliente (quando Tipo >= Cliente)
        public DateTime? DataPrimeiraCompra { get; set; }

        // Dados específicos de Administrador
        public DateTime? DataPromocaoAdmin { get; set; }

        // Relações
        public virtual ICollection<Pedido> HistoricoCompras { get; set; } = new List<Pedido>();
    }
}