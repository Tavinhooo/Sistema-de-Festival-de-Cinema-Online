namespace ProjetoES.API.DTOs
{
    public class FestivalRequestDTO
    {
        public string Nome { get; set; } = string.Empty;
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
    }
}