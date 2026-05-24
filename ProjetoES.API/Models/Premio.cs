namespace ProjetoES.API.Models
{
    /// <summary>
    /// Modelo de prémio, representando um prémio atribuído a um filme em um festival, incluindo informações sobre o nome do prémio,
    ///  descrição, data limite para votação e os votos associados ao prémio. O modelo de prémio é utilizado para representar os prémios
    ///  disponíveis em um festival e para armazenar as informações relacionadas aos votos dos clientes para cada prémio.
    /// </summary>
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