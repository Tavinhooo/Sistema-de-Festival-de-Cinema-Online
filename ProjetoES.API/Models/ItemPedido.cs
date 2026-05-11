using System.ComponentModel.DataAnnotations;

namespace ProjetoES.API.Models
{
    public class ItemPedido
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public virtual Pedido? Pedido { get; set; }


        public int FilmeId { get; set; }
        public virtual Filme? Filme { get; set; }
        
        public int Quantidade { get; set; } = 1;
        public string  TipoAcesso  { get; set; } = string.Empty;
        public double PrecoUnitario { get; set; }
    }
}
