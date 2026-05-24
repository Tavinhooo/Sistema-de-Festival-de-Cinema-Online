using ProjetoES.API.Models;

namespace ProjetoES.API.Factories;

/// <summary>
/// Fábrica concreta para criar instâncias de Acesso do tipo "Bilhete de Sessão",
///  que representa o acesso de um cliente a um filme adquirido por meio de bilhete de sessão.
///  Esta fábrica implementa o método CriarAcesso, que cria um objeto Acesso com base nos parâmetros fornecidos 
/// (ID do cliente, ID do filme e data de aquisição), 
/// definindo a data de validade para 30 dias após a data de aquisição e o tipo de acesso como "Bilhete de Sessão".
/// </summary>
public sealed class BilheteAcessoFactory : AcessoFactory
{
    public override Acesso CriarAcesso(int clienteId, int filmeId, DateTime dataAquisicao)
    {
        return new Acesso
        {
            ClienteId = clienteId,
            FilmeId = filmeId,
            DataAquisicao = dataAquisicao,
            DataValidade = dataAquisicao.AddDays(30),
            Estado = EstadoAcesso.Ativo,
            TipoAcesso = "Bilhete de Sessão"
        };
    }
}