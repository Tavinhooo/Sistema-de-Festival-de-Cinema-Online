using System.ComponentModel.DataAnnotations;

namespace ProjetoES.Models
{
    public enum EstadoCompra { Pendente, Confirmada, Falhada }

    public class Compra
    {
        [Key]
        public int Id { get; set; }
        
        public int ClienteId { get; set; }
        public virtual Cliente? Cliente { get; set; }
        
        public DateTime DataCompra { get; set; } = DateTime.Now;
        public double ValorTotal { get; set; }
        public string MetodoPagamento { get; set; } = string.Empty;
        public EstadoCompra Estado { get; set; } = EstadoCompra.Pendente;
    }
}