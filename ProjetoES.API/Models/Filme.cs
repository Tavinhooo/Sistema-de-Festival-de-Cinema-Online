using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoES.API.Models
{
    public class Filme
    {

        public int Id { get; set; }

        public string Titulo { get; set; } = string.Empty;

        public string Sinopse { get; set; } = string.Empty;
        public string Genero { get; set; } = string.Empty;

        public int Ano { get; set; }

        public int DuracaoMinutos { get; set; }

        public decimal PrecoBilhete { get; set; }
        public double MediaAvaliacao { get; set; }

        // Para já vamos usar um Link da internet para a imagem para ser mais fácil
        public string PosterUrl { get; set; } = string.Empty;
        public int FestivalId { get; set; }
        public Festival? Festival { get; set; }
    }
}