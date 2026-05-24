namespace ProjetoES.API.DTOs
{
    public class FestivalRequestDTO
    {
        public string Nome { get; set; } = string.Empty;
        public DateOnly DataInicio { get; set; }
        public DateOnly DataFim { get; set; }
    }
}