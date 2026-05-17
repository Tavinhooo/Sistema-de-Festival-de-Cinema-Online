using ProjetoES.API.Models;

namespace ProjetoES.API.Factories;

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