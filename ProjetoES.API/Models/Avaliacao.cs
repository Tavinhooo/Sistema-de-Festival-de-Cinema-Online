namespace ProjetoES.API.Models
{
    public class Avaliacao
    {
        public int Id { get; set; }

        // Fix: casing consistente + navigation property
        public int ClienteId { get; set; }
        public virtual Utilizador? Cliente { get; set; }

        public int FilmeId { get; set; }
        public virtual Filme? Filme { get; set; }

        // RF13: escala 1 a 10
        public int Nota { get; set; }
        public string Comentario { get; set; } = string.Empty;
        public DateTime DataAvaliacao { get; set; } = DateTime.UtcNow;
        public bool IsReportado { get; set; }
    }
}