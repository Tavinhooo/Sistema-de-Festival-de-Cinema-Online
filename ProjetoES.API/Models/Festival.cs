namespace ProjetoES.API.Models
{
    public class Festival
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public EstadoFestival  Estado { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public string Local { get; set; } = string.Empty;
        
        // Navigation: films in this festival
        public virtual ICollection<Filme> Filmes { get; set; } = new List<Filme>();
    }
}