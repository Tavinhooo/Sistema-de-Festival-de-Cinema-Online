using ProjetoES.API.Models;

namespace ProjetoES.API.Factories;

public sealed class PasseDiarioAcessoFactory : AcessoFactory
{
    public override Acesso CriarAcesso(int clienteId, int filmeId, DateTime dataAquisicao)
    {
        return new Acesso
        {
            ClienteId = clienteId,
            FilmeId = filmeId,
            DataAquisicao = dataAquisicao,
            DataValidade = dataAquisicao.AddDays(1),
            Estado = EstadoAcesso.Ativo,
            TipoAcesso = "Passe diário"
        };
    }
}