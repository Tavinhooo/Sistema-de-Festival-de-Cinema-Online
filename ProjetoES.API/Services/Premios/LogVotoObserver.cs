using ProjetoES.API.Data;
using ProjetoES.API.Models;

namespace ProjetoES.API.Services.Premios;

/// <summary>
/// Observer que escreve no log sempre que um voto é submetido.
/// </summary>
public class LogVotoObserver : IVotoObserver
{
    private readonly ILogger<LogVotoObserver> _logger;

    public LogVotoObserver(ILogger<LogVotoObserver> logger)
    {
        _logger = logger;
    }

    public void OnVotoSubmetido(VotoPremio voto, AppDbContext context)
    {
        var filme = context.Filmes.Find(voto.FilmeId);
        var premio = context.Set<Premio>().Find(voto.PremioId);

        _logger.LogInformation(
            "[Prémios] ClienteId={ClienteId} votou em FilmeId={FilmeId} ({FilmeTitulo}) " +
            "para o prémio '{PremioNome}' (PremioId={PremioId}) em {DataVoto:u}",
            voto.ClienteId,
            voto.FilmeId,
            filme?.Titulo ?? "?",
            premio?.Nome ?? "?",
            voto.PremioId,
            voto.DataVoto);
    }
}