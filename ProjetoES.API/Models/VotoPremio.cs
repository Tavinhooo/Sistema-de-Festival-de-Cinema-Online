namespace ProjetoES.API.Models
{
    public class VotoPremio
    {
        public int Id { get; set; }

        public int PremioId { get; set; }
        public virtual Premio? Premio { get; set; }

        /// <summary>Filme em que o utilizador votou.</summary>
        public int FilmeId { get; set; }
        public virtual Filme? Filme { get; set; }

        /// <summary>Utilizador que votou.</summary>
        public int ClienteId { get; set; }
        public virtual Utilizador? Cliente { get; set; }

        public DateTime DataVoto { get; set; } = DateTime.UtcNow;
    }
}