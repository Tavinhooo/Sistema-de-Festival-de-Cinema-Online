namespace ProjetoES.API.DTOs;
/// <summary>
/// DTO para vincular um filme a um festival, incluindo o preço do bilhete para assistir ao filme durante o festival.
/// </summary>
public class VincularFilmeFestivalDTO
{
    public decimal PrecoBilhete { get; set; }
}
