using ProjetoES.API.Models;

namespace ProjetoES.API.Factories;

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