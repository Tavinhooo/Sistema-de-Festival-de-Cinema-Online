using ProjetoES.API.Models;

namespace ProjetoES.API.Factories;

public sealed class AluguerDigitalFactory : AcessoFactory
{
    public override Acesso CriarAcesso(int clienteId, int filmeId, DateTime dataAquisicao)
    {
        return new Acesso
        {
            ClienteId = clienteId,
            FilmeId = filmeId,
            DataAquisicao = dataAquisicao,
            DataValidade = dataAquisicao.AddHours(48),
            Estado = EstadoAcesso.Ativo,
            TipoAcesso = "Aluguer Digital"
        };
    }
}