using System.ComponentModel.DataAnnotations;

namespace ProjetoES.API.Models
{
    public class ItemCarrinho
    {
        public int Id { get; set; }
        public int FilmeId { get; set; }
        public int CarrinhoId { get; set; }

        public virtual Filme? Filme { get; set; }
        
        public int Quantidade { get; set; }
        public double PrecoUnitario { get; set; }
        public string TipoAcesso { get; set; } = string.Empty;
    }
}