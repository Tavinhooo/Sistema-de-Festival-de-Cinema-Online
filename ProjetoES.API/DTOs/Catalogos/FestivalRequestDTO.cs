namespace ProjetoES.API.DTOs;
/// <summary>
/// DTO para criar ou atualizar um festival, incluindo o nome e as datas de início e fim do festival.
/// </summary>
    public class FestivalRequestDTO
    {
        public string Nome { get; set; } = string.Empty;
        public DateOnly DataInicio { get; set; }
        public DateOnly DataFim { get; set; }
    }
