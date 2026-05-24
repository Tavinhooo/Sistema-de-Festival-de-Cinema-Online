namespace ProjetoES.API.DTOs
{

    /// <summary>
    /// DTO para a resposta de um festival, incluindo informações detalhadas como nome, descrição, datas, estado e local do festival.
    /// </summary>
    public class FestivalResponseDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public DateOnly DataInicio { get; set; }
        public DateOnly DataFim { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string Local { get; set; } = string.Empty;
    }
}