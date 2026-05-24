using ProjetoES.API.Data;
using ProjetoES.API.Models;

namespace ProjetoES.API.Services.Recomendacoes;

/// <summary>
/// Recomenda filmes com base nos géneros que o utilizador mais avaliou positivamente (>= 7).
/// </summary>
public class PorGeneroStrategy : IRecomendacaoStrategy
{
    private const int NotaMinima = 7;
    private const int MaxResultados = 10;

    public string Nome => "Por Género";

    public IEnumerable<Filme> Recomendar(int userId, IEnumerable<Filme> filmesDisponiveis, AppDbContext context)
    {
        // Filmes que o utilizador já avaliou
        var filmesAvaliados = context.Avaliacoes
            .Where(a => a.ClienteId == userId)
            .ToList();

        var idsJaAvaliados = filmesAvaliados.Select(a => a.FilmeId).ToHashSet();

        // Géneros preferidos: géneros de filmes com nota >= 7, ordenados por frequência
        var generospreferidos = filmesAvaliados
            .Where(a => a.Classificacao >= NotaMinima && a.Filme != null)
            .GroupBy(a => a.Filme!.Genero)
            .OrderByDescending(g => g.Count())
            .Select(g => g.Key)
            .ToList();

        // Se não tiver avaliações positivas, usa todos os géneros avaliados
        if (!generospreferidos.Any())
        {
            generospreferidos = filmesAvaliados
                .Where(a => a.Filme != null)
                .Select(a => a.Filme!.Genero)
                .Distinct()
                .ToList();
        }

        // Filmes do género preferido que ainda não avaliou, por média de avaliação
        return filmesDisponiveis
            .Where(f => !idsJaAvaliados.Contains(f.Id) && generospreferidos.Contains(f.Genero))
            .OrderByDescending(f => f.MediaAvaliacao)
            .Take(MaxResultados);
    }
}