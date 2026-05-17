using System.Collections.Generic;

namespace ProjetoES.API.Models
{
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