using ProjetoES.API.Data;
using ProjetoES.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ProjetoES.API.Services.Premios;

/// <summary>
/// Observer que recalcula o líder do prémio após cada voto e regista no log.
/// </summary>
public class LiderPremioObserver : IVotoObserver
{
    private readonly ILogger<LiderPremioObserver> _logger;

    public LiderPremioObserver(ILogger<LiderPremioObserver> logger)
    {
        _logger = logger;
    }

    public void OnVotoSubmetido(VotoPremio voto, AppDbContext context)
    {
        // Agrupa os votos do prémio e encontra o filme com mais votos
        var lider = context.Set<VotoPremio>()
            .Where(v => v.PremioId == voto.PremioId)
            .Include(v => v.Filme)
            .AsEnumerable()
            .GroupBy(v => v.FilmeId)
            .Select(g => new { FilmeId = g.Key, Total = g.Count(), Titulo = g.First().Filme?.Titulo })
            .OrderByDescending(x => x.Total)
            .FirstOrDefault();

        if (lider != null)
        {
            _logger.LogInformation(
                "[Prémios] Líder atual do PremioId={PremioId}: '{FilmeTitulo}' com {Total} voto(s).",
                voto.PremioId,
                lider.Titulo ?? "?",
                lider.Total);
        }
    }
}