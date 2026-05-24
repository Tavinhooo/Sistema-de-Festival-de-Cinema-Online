using ProjetoES.API.Data;
using ProjetoES.API.Models;

namespace ProjetoES.API.Services.Premios;

/// <summary>
/// Padrão Observer — notificado sempre que um voto é submetido ou alterado.
/// </summary>
public interface IVotoObserver
{
    void OnVotoSubmetido(VotoPremio voto, AppDbContext context);
}