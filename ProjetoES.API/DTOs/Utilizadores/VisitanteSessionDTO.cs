namespace ProjetoES.API.DTOs;
/// <summary>
/// DTO para a sessão de um visitante, incluindo o ID do visitante e se está logado ou não.
/// </summary>
public class VisitanteSessionDTO
{
    public int VisitanteId { get; set; }
    public bool IsLogged { get; set; }
}
