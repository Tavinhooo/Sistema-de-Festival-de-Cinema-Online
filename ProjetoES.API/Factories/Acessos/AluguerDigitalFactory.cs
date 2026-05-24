using ProjetoES.API.Models;

namespace ProjetoES.API.Factories;

/// <summary>
/// Fábrica concreta para criar instâncias de Acesso do tipo "Aluguer Digital",
/// que representa o acesso de um cliente a um filme adquirido por meio de aluguer digital.
/// Esta fábrica implementa o método CriarAcesso,
/// que cria um objeto Acesso com base nos parâmetros fornecidos (ID do cliente, ID do filme e data de aquisição),
/// definindo a data de validade para 48 horas após a data de aquisição e o tipo de acesso como "Aluguer Digital".
/// </summary>
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