using System.ComponentModel.DataAnnotations;

namespace ProjetoES.Models
{
    public class Festival
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do festival é obrigatório.")]
        public string Nome { get; set; } = string.Empty;

        public string Descricao { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime DataInicio { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataFim { get; set; }

        public string PosterUrl { get; set; } = string.Empty;

        // Relacionamento: Um Festival tem muitos Filmes
        public List<Filme> Filmes { get; set; } = new();
    }
}