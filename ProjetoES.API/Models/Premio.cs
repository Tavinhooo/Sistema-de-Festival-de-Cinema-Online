namespace ProjetoES.API.Models
{
    public class Premio
    {
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;         // ex: "Melhor Filme"
        public string Descricao { get; set; } = string.Empty;

        public int FestivalId { get; set; }
        public virtual Festival? Festival { get; set; }

        /// <summary>Data limite para votar. Null = até ao fim do festival.</summary>
        public DateTime? DataLimiteVotacao { get; set; }

        public virtual ICollection<VotoPremio> Votos { get; set; } = new List<VotoPremio>();
    }
}