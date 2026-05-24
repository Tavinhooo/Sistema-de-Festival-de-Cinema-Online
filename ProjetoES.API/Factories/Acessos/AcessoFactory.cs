using ProjetoES.API.Models;

namespace ProjetoES.API.Factories;
/// <summary>
/// Fábrica abstrata para criar instâncias de Acesso,
///  que representa o acesso de um cliente a um filme adquirido.
///  Esta fábrica define um método abstrato CriarAcesso,
///  que deve ser implementado por classes concretas para criar objetos Acesso com base nos parâmetros fornecidos 
/// (ID do cliente, ID do filme e data de aquisição).
/// </summary>
public abstract class AcessoFactory
{
    public abstract Acesso CriarAcesso(int clienteId, int filmeId, DateTime dataAquisicao);
}