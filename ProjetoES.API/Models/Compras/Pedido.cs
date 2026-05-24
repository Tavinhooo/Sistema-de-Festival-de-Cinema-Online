using System.ComponentModel.DataAnnotations;

namespace ProjetoES.API.Models
{
    public enum EstadoPedido { Pendente, Completo, Cancelado }

    /// <summary>
    /// Modelo de pedido, representando um pedido de compra realizado por um cliente, incluindo informações sobre o cliente, data do pedido,
    /// </summary>
    public class Pedido
    {
        public int Id { get; set; }

        public int UtilizadorId { get; set; }
        public virtual Utilizador? Utilizador { get; set; }

        public DateTime DataPedido { get; set; } = DateTime.UtcNow;
        public DateTime? DataPagamento { get; set; }
        public int? SessaoId { get; set; }
        public virtual Sessao? Sessao { get; set; }
        public int Quantidade { get; set; }


        public double PrecoTotal { get; set; }
        public EstadoPedido Estado { get; set; } = EstadoPedido.Pendente;

        public virtual ICollection<ItemPedido> Itens { get; set; } = new List<ItemPedido>();
    }
}