using ProjetoES.API.Models;

namespace ProjetoES.API.Factories;
/// <summary>
/// Fábrica concreta para criar instâncias de Acesso do tipo "Passe Completo", que representa o acesso de um cliente a um filme adquirido por meio de passe completo. Esta fábrica implementa o método CriarAcesso, que cria um objeto Acesso com base nos parâmetros fornecidos (ID do cliente, ID do filme e data de aquisição), definindo a data de validade como null (acesso ilimitado) e o tipo de acesso como "Passe completo".
/// </summary>
public sealed class PasseCompletoAcessoFactory : AcessoFactory
{
    public override Acesso CriarAcesso(int clienteId, int filmeId, DateTime dataAquisicao)
    {
        return new Acesso
        {
            ClienteId = clienteId,
            FilmeId = filmeId,
            DataAquisicao = dataAquisicao,
            DataValidade = null,
            Estado = EstadoAcesso.Ativo,
            TipoAcesso = "Passe completo"
        };
    }
}