using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoES.Models
{
    public class Filme
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O título é obrigatório.")]
        [StringLength(100, ErrorMessage = "O título não pode exceder 100 caracteres.")]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "A sinopse é obrigatória.")]
        public string Sinopse { get; set; } = string.Empty;

        [Required(ErrorMessage = "O género é obrigatório.")]
        public string Genero { get; set; } = string.Empty;

        [Range(1888, 2100, ErrorMessage = "Ano de lançamento inválido.")]
        public int Ano { get; set; }

        [Range(1, 600, ErrorMessage = "A duração deve ser entre 1 e 600 minutos.")]
        public int DuracaoMinutos { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, 100.00, ErrorMessage = "O preço do bilhete deve ser maior que 0.")]
        public decimal PrecoBilhete { get; set; }

        // Para já vamos usar um Link da internet para a imagem para ser mais fácil
        public string PosterUrl { get; set; } = string.Empty; 
    }
}