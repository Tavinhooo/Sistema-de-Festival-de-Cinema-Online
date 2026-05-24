namespace ProjetoES.API.Models
{
    /// <summary>
    /// Modelo de voto de prémio, representando um voto atribuído por um cliente a um filme para um determinado prémio em um festival,
    ///  incluindo informações sobre o prémio, o filme, o cliente que votou e a data do voto.
    ///  O modelo de voto de prémio é utilizado para armazenar os votos dos clientes para os prémios disponíveis em um festival
    ///  e para calcular os resultados dos prémios com base nos votos recebidos.
    /// </summary>
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