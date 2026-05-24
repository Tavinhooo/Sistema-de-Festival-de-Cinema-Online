using Microsoft.EntityFrameworkCore;
using ProjetoES.API.Data;
using ProjetoES.API.Models;

namespace ProjetoES.API.Repositories;

/// <summary>
/// Repositório de clientes, responsável por gerenciar as operações relacionadas aos clientes, incluindo a obtenção do histórico de compras,
///  a gestão dos acessos reservados, a criação e atualização de avaliações, a promoção de membros a clientes e a consulta de informações do cliente, bem como a eliminação de avaliações.
/// </summary>
public class ClienteRepository
{
    private readonly AppDbContext _context;

    public ClienteRepository(AppDbContext context)
    {
        _context = context;
    }

    // RF07 - Histórico de compras
    public List<Pedido> ObterHistoricoCompras(int clienteId)
    {
        return _context.Set<Pedido>()
            .Include(p => p.Itens)
                .ThenInclude(i => i.Filme)
            .Where(p => p.UtilizadorId == clienteId)
            .OrderByDescending(p => p.DataPedido)
            .ToList();
    }

    // RU09 - Acessos reservados
    public List<Acesso> ObterAcessos(int clienteId)
    {
        return _context.Set<Acesso>()
            .Include(a => a.Filme)
            .Where(a => a.ClienteId == clienteId && a.Estado == EstadoAcesso.Ativo)
            .OrderByDescending(a => a.DataAquisicao)
            .ToList();
    }

    // RF13 - Votar num filme
    public bool JaAvaliou(int clienteId, int filmeId)
    {
        return _context.Set<Avaliacao>()
            .Any(a => a.ClienteId == clienteId && a.FilmeId == filmeId);
    }

    public Avaliacao CriarAvaliacao(Avaliacao avaliacao)
    {
        _context.Set<Avaliacao>().Add(avaliacao);
        _context.SaveChanges();
        return avaliacao;
    }

    public List<Avaliacao> ObterAvaliacoesDoCliente(int clienteId)
    {
        return _context.Set<Avaliacao>()
            .Include(a => a.Filme)
            .Where(a => a.ClienteId == clienteId)
            .OrderByDescending(a => a.DataAvaliacao)
            .ToList();
    }

    // RF04 - Promover Membro a Cliente
    public void PromoverParaCliente(Utilizador utilizador)
    {
        utilizador.Tipo = TipoUtilizador.Cliente;
        utilizador.DataPrimeiraCompra = DateTime.UtcNow;
        _context.Set<Utilizador>().Update(utilizador);
        _context.SaveChanges();
    }

    public Utilizador? ObterPorId(int id)
    {
        return _context.Set<Utilizador>().FirstOrDefault(u => u.Id == id);
    }

    // RF15.2 - Editar/apagar próprias avaliações
    public Avaliacao? ObterAvaliacaoPorId(int id)
    {
        return _context.Set<Avaliacao>()
            .Include(a => a.Filme)
            .FirstOrDefault(a => a.Id == id);
    }

    public Avaliacao AtualizarAvaliacao(Avaliacao avaliacao)
    {
        _context.Set<Avaliacao>().Update(avaliacao);
        _context.SaveChanges();
        return avaliacao;
    }

    public void EliminarAvaliacao(int id)
    {
        var avaliacao = _context.Set<Avaliacao>().Find(id);
        if (avaliacao != null)
        {
            _context.Set<Avaliacao>().Remove(avaliacao);
            _context.SaveChanges();
        }
    }
}