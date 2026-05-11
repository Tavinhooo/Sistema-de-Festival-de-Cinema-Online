using System.ComponentModel.DataAnnotations;

namespace ProjetoES.API.Models
{
    public class Carrinho
    {
        public int Id { get; set; }
        
        public int UtilizadorId { get; set; }
        public DateTime DataCriacao { get; set; }
        
        // Relação 1 para muitos com os Itens
        public virtual ICollection<ItemCarrinho> Itens { get; set; } = new List<ItemCarrinho>();
    }
}