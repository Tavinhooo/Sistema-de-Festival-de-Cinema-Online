namespace ProjetoES.API.Models
{
    /// <summary>
    /// Modelo de avaliação, representando a avaliação de um filme por um cliente, incluindo informações sobre o cliente, filme,
    ///  classificação, comentário, data da avaliação e informações sobre reportes de avaliações.
    /// </summary>
    public class Avaliacao
    {
        public int Id { get; set; }

        // Fix: casing consistente + navigation property
        public int ClienteId { get; set; }
        public virtual Utilizador? Cliente { get; set; }

        public int FilmeId { get; set; }
        public virtual Filme? Filme { get; set; }

        // RF13: escala 1 a 10
        public int Classificacao { get; set; }
        public string Comentario { get; set; } = string.Empty;
        public DateTime DataAvaliacao { get; set; } = DateTime.UtcNow;
        public bool IsReportado { get; set; }
        public string? MotivoReporte { get; set; }
    }
}