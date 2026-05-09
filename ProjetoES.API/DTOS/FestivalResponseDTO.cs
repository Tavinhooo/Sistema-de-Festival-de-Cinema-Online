namespace ProjetoES.API.DTOs
{
    public class FestivalResponseDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string Estado { get; set; } = string.Empty;
    }
}