namespace ProjetoES.API.Models
{
    public class Festival
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public EstadoFestival  Estado { get; set; }
    }
}