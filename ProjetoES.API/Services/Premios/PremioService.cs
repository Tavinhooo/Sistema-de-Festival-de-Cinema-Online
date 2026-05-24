using Microsoft.EntityFrameworkCore;
using ProjetoES.API.Data;
using ProjetoES.API.Models;

namespace ProjetoES.API.Services.Premios;

public class PremioService
{
    private readonly AppDbContext _context;
    private readonly IEnumerable<IVotoObserver> _observers;

    public PremioService(AppDbContext context, IEnumerable<IVotoObserver> observers)
    {
        _context = context;
        _observers = observers;
    }

    // ── Notificar observers ──────────────────────────────────────────────
    private void Notificar(VotoPremio voto)
    {
        foreach (var observer in _observers)
            observer.OnVotoSubmetido(voto, _context);
    }

    // ── Prémios ─────────────────────────────────────────────────────────

    public List<Premio> ObterPremiosPorFestival(int festivalId) =>
        _context.Set<Premio>()
            .Include(p => p.Votos)
            .Where(p => p.FestivalId == festivalId)
            .ToList();

    public Premio ObterPremioPorId(int premioId) =>
        _context.Set<Premio>()
            .Include(p => p.Votos)
            .FirstOrDefault(p => p.Id == premioId)
            ?? throw new ArgumentException("Prémio não encontrado.");

    public Premio CriarPremio(int festivalId, string nome, string descricao, DateTime? dataLimite)
    {
        var festival = _context.Festivais.Find(festivalId)
            ?? throw new ArgumentException("Festival não encontrado.");

        var premio = new Premio
        {
            FestivalId = festivalId,
            Nome = nome,
            Descricao = descricao,
            DataLimiteVotacao = dataLimite
        };

        _context.Set<Premio>().Add(premio);
        _context.SaveChanges();
        return premio;
    }

    public void EliminarPremio(int premioId)
    {
        var premio = _context.Set<Premio>().Find(premioId)
            ?? throw new ArgumentException("Prémio não encontrado.");
        _context.Set<Premio>().Remove(premio);
        _context.SaveChanges();
    }

    // ── Votação ──────────────────────────────────────────────────────────

    /// <summary>
    /// Regista ou atualiza o voto do utilizador para um prémio.
    /// Um utilizador só pode ter 1 voto por prémio — mudar o voto substitui o anterior.
    /// </summary>
    public VotoPremio Votar(int premioId, int clienteId, int filmeId)
    {
        var premio = _context.Set<Premio>()
            .Include(p => p.Festival)
            .FirstOrDefault(p => p.Id == premioId)
            ?? throw new ArgumentException("Prémio não encontrado.");

        // Verificar prazo de votação
        if (premio.DataLimiteVotacao.HasValue && DateTime.UtcNow > premio.DataLimiteVotacao.Value)
            throw new InvalidOperationException("O período de votação para este prémio já terminou.");

        // Verificar que o filme pertence ao festival do prémio
        var filmeNofestival = _context.Set<FestivalFilme>()
            .Any(ff => ff.FestivalId == premio.FestivalId && ff.FilmeId == filmeId);
        if (!filmeNofestival)
            throw new ArgumentException("O filme não pertence ao festival deste prémio.");

        // Um voto por utilizador por prémio — atualiza se já existe
        var votoExistente = _context.Set<VotoPremio>()
            .FirstOrDefault(v => v.PremioId == premioId && v.ClienteId == clienteId);

        if (votoExistente != null)
        {
            votoExistente.FilmeId = filmeId;
            votoExistente.DataVoto = DateTime.UtcNow;
            _context.SaveChanges();
            Notificar(votoExistente);
            return votoExistente;
        }

        var novoVoto = new VotoPremio
        {
            PremioId = premioId,
            ClienteId = clienteId,
            FilmeId = filmeId,
            DataVoto = DateTime.UtcNow
        };

        _context.Set<VotoPremio>().Add(novoVoto);
        _context.SaveChanges();
        Notificar(novoVoto);
        return novoVoto;
    }

    /// <summary>
    /// Devolve a classificação completa de um prémio — filmes ordenados por votos.
    /// </summary>
    public List<ResultadoFilmeDTO> ObterResultados(int premioId)
    {
        var votos = _context.Set<VotoPremio>()
            .Include(v => v.Filme)
            .Where(v => v.PremioId == premioId)
            .ToList();

        return votos
            .GroupBy(v => v.FilmeId)
            .Select(g => new ResultadoFilmeDTO
            {
                FilmeId = g.Key,
                FilmeTitulo = g.First().Filme?.Titulo ?? "?",
                PosterUrl = g.First().Filme?.PosterUrl ?? "",
                TotalVotos = g.Count()
            })
            .OrderByDescending(r => r.TotalVotos)
            .ToList();
    }
    public void ToggleVotacao(int premioId, bool aberta)
    {
        var premio = _context.Premios.Find(premioId)
            ?? throw new ArgumentException("Prémio não encontrado.");
        premio.DataLimiteVotacao = aberta
            ? DateTime.UtcNow.AddMonths(1)
            : DateTime.UtcNow.AddDays(-1);
        _context.SaveChanges();
    }

    /// <summary>Voto atual do utilizador para um prémio (null se ainda não votou).</summary>
    public VotoPremio? ObterVotoUtilizador(int premioId, int clienteId) =>
        _context.Set<VotoPremio>()
            .FirstOrDefault(v => v.PremioId == premioId && v.ClienteId == clienteId);
}

// ── DTO local ────────────────────────────────────────────────────────────────
public class ResultadoFilmeDTO
{
    public int FilmeId { get; set; }
    public string FilmeTitulo { get; set; } = string.Empty;
    public string PosterUrl { get; set; } = string.Empty;
    public int TotalVotos { get; set; }
}