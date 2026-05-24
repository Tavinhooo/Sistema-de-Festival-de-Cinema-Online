using ProjetoES.API.Data;
using ProjetoES.API.Models;

namespace ProjetoES.API.Services.Recomendacoes;

/// <summary>
/// Padrão Strategy — cada implementação define um critério de recomendação diferente.
/// </summary>
public interface IRecomendacaoStrategy
{
    /// <summary>Nome da estratégia (ex: "Por Género").</summary>
    string Nome { get; }

    /// <summary>
    /// Devolve filmes recomendados para o utilizador, excluindo os que já avaliou.
    /// </summary>
    IEnumerable<Filme> Recomendar(int userId, IEnumerable<Filme> filmesDisponiveis, AppDbContext context);
}