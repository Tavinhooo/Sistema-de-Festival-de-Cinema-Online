using System.ComponentModel.DataAnnotations;

namespace ProjetoES.Models
{
    public class Carrinho
    {
        [Key]
        public int Id { get; set; }
        
        public int UtilizadorId { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        
        // Relação 1 para muitos com os Itens
        public virtual ICollection<ItemCarrinho> Itens { get; set; } = new List<ItemCarrinho>();
    }
}