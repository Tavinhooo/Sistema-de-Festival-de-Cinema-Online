namespace ProjetoES.API.DTOs
{
    public class FestivalResponseDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public DateOnly DataInicio { get; set; }
        public DateOnly DataFim { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string Local { get; set; } = string.Empty;
    }
}