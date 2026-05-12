using System.Collections.Generic;

namespace ProjetoES.API.Models
{
    // Classe base abstrata para TPH (Table Per Hierarchy)
    // Todos os tipos de utilizadores usam a tabela "Visitantes" com discriminador
    public abstract class UtilizadorBase
    {
        public int Id { get; set; }
        public bool IsLogged { get; set; }
        
        // Campos para utilizadores autenticados (Utilizador, Membro)
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string PrimeiroNome { get; set; } = string.Empty;
        public string UltimoNome { get; set; } = string.Empty;
        public string? MetodoPagamento { get; set; }
    }

    // Utilizador autenticado (TPH - herda de UtilizadorBase)
    public class Utilizador : UtilizadorBase
    {
        public TipoUtilizador Tipo { get; set; } = TipoUtilizador.Membro;
        
        // Dados específicos de Cliente (quando Tipo >= Cliente)
        public DateTime? DataPrimeiraCompra { get; set; }
        
        // Dados específicos de Administrador (quando Tipo == Administrador)
        public DateTime? DataPromocaoAdmin { get; set; }
        
        // Relações
        public virtual ICollection<Pedido> HistoricoCompras { get; set; } = new List<Pedido>();
    }
}