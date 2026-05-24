using ProjetoES.API.Data;
using ProjetoES.API.Models;

namespace ProjetoES.API.Services.Recomendacoes;

/// <summary>
/// Recomenda filmes mais populares — maior número de avaliações e melhor média.
/// </summary>
public class PorPopularidadeStrategy : IRecomendacaoStrategy
{
    private const int MaxResultados = 10;

    public string Nome => "Por Popularidade";

    public IEnumerable<Filme> Recomendar(int userId, IEnumerable<Filme> filmesDisponiveis, AppDbContext context)
    {
        var idsJaAvaliados = context.Avaliacoes
            .Where(a => a.ClienteId == userId)
            .Select(a => a.FilmeId)
            .ToHashSet();

        // Contar avaliações por filme
        var avaliacoesPorFilme = context.Avaliacoes
            .GroupBy(a => a.FilmeId)
            .Select(g => new { FilmeId = g.Key, TotalAvaliacoes = g.Count() })
            .ToDictionary(x => x.FilmeId, x => x.TotalAvaliacoes);

        return filmesDisponiveis
            .Where(f => !idsJaAvaliados.Contains(f.Id))
            .OrderByDescending(f =>
            {
                // Score: 70% média de avaliação (0-10) + 30% popularidade (normalizada a 10)
                var totalAvaliacoes = avaliacoesPorFilme.GetValueOrDefault(f.Id, 0);
                var popularidadeNorm = Math.Min(totalAvaliacoes / 10.0, 10.0);
                return (f.MediaAvaliacao * 0.7) + (popularidadeNorm * 0.3);
            })
            .Take(MaxResultados);
    }
}