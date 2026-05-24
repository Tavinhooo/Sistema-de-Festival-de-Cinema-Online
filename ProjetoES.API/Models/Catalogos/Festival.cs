namespace ProjetoES.API.Models
{
    /// <summary>
    /// Modelo de festival, representando um festival de cinema, incluindo informações sobre o nome, datas, estado, descrição,
    ///  local e os filmes associados ao festival.
    /// </summary>
    public class Festival
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public DateOnly DataInicio { get; set; }
        public DateOnly DataFim { get; set; }
        public EstadoFestival  Estado { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public string Local { get; set; } = string.Empty;
        
        // Navigation: films in this festival
        public virtual ICollection<Filme> Filmes { get; set; } = new List<Filme>();
    }
}