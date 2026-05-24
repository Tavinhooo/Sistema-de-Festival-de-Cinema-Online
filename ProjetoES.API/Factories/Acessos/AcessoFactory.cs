using ProjetoES.API.Models;

namespace ProjetoES.API.Factories;

public abstract class AcessoFactory
{
    public abstract Acesso CriarAcesso(int clienteId, int filmeId, DateTime dataAquisicao);
}