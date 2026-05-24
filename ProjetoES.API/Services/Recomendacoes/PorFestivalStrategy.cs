using ProjetoES.API.Data;
using ProjetoES.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ProjetoES.API.Services.Recomendacoes;

/// <summary>
/// Recomenda filmes de festivais que o utilizador já frequentou mas ainda não viu todos os filmes.
/// Para visitantes/membros, sugere filmes dos festivais activos.
/// </summary>
public class PorFestivalStrategy : IRecomendacaoStrategy
{
    private const int MaxResultados = 10;

    public string Nome => "Por Festival";

    public IEnumerable<Filme> Recomendar(int userId, IEnumerable<Filme> filmesDisponiveis, AppDbContext context)
    {
        var idsJaAvaliados = context.Avaliacoes
            .Where(a => a.ClienteId == userId)
            .Select(a => a.FilmeId)
            .ToHashSet();

        // Festivais onde o utilizador já tem acesso (comprou passe)
        var festivaisComAcesso = context.Acessos
            .Where(a => a.ClienteId == userId)
            .Select(a => a.FestivalId)
            .Distinct()
            .ToHashSet();

        List<int> festivaisAlvo;

        if (festivaisComAcesso.Any())
        {
            // Utilizador com acessos: recomendar filmes dos seus festivais que ainda não avaliou
            festivaisAlvo = festivaisComAcesso.ToList();
        }
        else
        {
            // Sem acessos: recomendar filmes dos festivais a decorrer
            var hoje = DateOnly.FromDateTime(DateTime.UtcNow);
            festivaisAlvo = context.Festivais
                .Where(f => f.DataInicio <= hoje && f.DataFim >= hoje)
                .Select(f => f.Id)
                .ToList();
        }

        // IDs dos filmes nesses festivais
        var idsFilmesFestivais = context.Set<FestivalFilme>()
            .Where(ff => festivaisAlvo.Contains(ff.FestivalId))
            .Select(ff => ff.FilmeId)
            .ToHashSet();

        return filmesDisponiveis
            .Where(f => !idsJaAvaliados.Contains(f.Id) && idsFilmesFestivais.Contains(f.Id))
            .OrderByDescending(f => f.MediaAvaliacao)
            .Take(MaxResultados);
    }
}