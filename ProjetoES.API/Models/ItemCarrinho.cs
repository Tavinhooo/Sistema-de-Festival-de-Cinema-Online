using System.ComponentModel.DataAnnotations;

namespace ProjetoES.API.Models
{
    public class ItemCarrinho
    {
        public int Id { get; set; }
        
        public int FilmeId { get; set; }
        public virtual Filme? Filme { get; set; }
        
        public int Quantidade { get; set; } = 1;
        public double PrecoUnitario { get; set; }
        
        // Para ligar ao Carrinho
        public int CarrinhoId { get; set; }
    }
}