using Microsoft.EntityFrameworkCore;
using ProjetoES.API.Data;
using ProjetoES.API.Models;

namespace ProjetoES.API.Services.Recomendacoes;

/// <summary>
/// Orquestra as estratégias de recomendação.
/// Executa todas e agrega os resultados, evitando duplicados.
/// </summary>
public class RecomendacoesService
{
    private readonly AppDbContext _context;
    private readonly IEnumerable<IRecomendacaoStrategy> _estrategias;

    public RecomendacoesService(AppDbContext context, IEnumerable<IRecomendacaoStrategy> estrategias)
    {
        _context = context;
        _estrategias = estrategias;
    }

    /// <summary>
    /// Devolve recomendações por estratégia para o utilizador dado.
    /// Cada estratégia contribui com até 10 filmes, sem repetições entre estratégias.
    /// </summary>
    public Dictionary<string, IEnumerable<Filme>> ObterRecomendacoes(int userId)
    {
        // Carrega todos os filmes com os seus festivais para evitar N+1
        var todosFilmes = _context.Filmes
            .Include(f => f.Festivais)
            .ToList();

        var resultado = new Dictionary<string, IEnumerable<Filme>>();
        var idsJaMostrados = new HashSet<int>();

        foreach (var estrategia in _estrategias)
        {
            var recomendados = estrategia
                .Recomendar(userId, todosFilmes, _context)
                .Where(f => !idsJaMostrados.Contains(f.Id))
                .ToList();

            resultado[estrategia.Nome] = recomendados;

            foreach (var f in recomendados)
                idsJaMostrados.Add(f.Id);
        }

        return resultado;
    }

    /// <summary>
    /// Devolve recomendações de uma estratégia específica pelo nome.
    /// </summary>
    public IEnumerable<Filme> ObterRecomendacoesPorEstrategia(int userId, string nomeEstrategia)
    {
        var estrategia = _estrategias
            .FirstOrDefault(e => e.Nome.Equals(nomeEstrategia, StringComparison.OrdinalIgnoreCase))
            ?? throw new ArgumentException($"Estratégia '{nomeEstrategia}' não encontrada.");

        var todosFilmes = _context.Filmes
            .Include(f => f.Festivais)
            .ToList();

        return estrategia.Recomendar(userId, todosFilmes, _context);
    }
}