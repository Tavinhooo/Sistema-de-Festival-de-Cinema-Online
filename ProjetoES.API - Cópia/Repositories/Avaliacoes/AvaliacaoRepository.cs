using Microsoft.EntityFrameworkCore;
using ProjetoES.API.Data;
using ProjetoES.API.Models;
namespace ProjetoES.API.Repositories;

public class AvaliacaoRepository
{
    private readonly AppDbContext _context;
    public AvaliacaoRepository(AppDbContext context)
    {
        _context = context;
    }
    public List<Avaliacao> ObterTodasAvaliacoes()
    {
        return _context.Avaliacoes.ToList();
    }
    public Avaliacao? ObterAvaliacaoPorId(int id)
    {
        return _context.Avaliacoes.Find(id);
    }
    public List<Avaliacao> ObterAvaliacoesPorFilme(int filmeId)
    {
        return _context.Avaliacoes
            .Include(a => a.Cliente)
            .Where(a => a.FilmeId == filmeId)
            .ToList();
    }
    public List<Avaliacao> ObterAvaliacoesPorCliente(int clienteId)
    {
        return _context.Avaliacoes.Where(a => a.ClienteId == clienteId).ToList();
    }
    public List<Avaliacao> ObterAvaliacoesReportadas()
    {
        return _context.Avaliacoes.Where(a => a.IsReportado).ToList();
    }
    public void AdicionarAvaliacao(Avaliacao avaliacao)
    {
        _context.Avaliacoes.Add(avaliacao);
        _context.SaveChanges();
    }
    public void AtualizarAvaliacao(Avaliacao avaliacao)
    {
        _context.Avaliacoes.Update(avaliacao);
        _context.SaveChanges();
    }
    public void EliminarAvaliacao(Avaliacao avaliacao)
    {
        _context.Avaliacoes.Remove(avaliacao);
        _context.SaveChanges();
    }
    public Avaliacao? ObterAvaliacaoPorIdComCliente(int id)
{
    return _context.Avaliacoes
        .Include(a => a.Cliente)
        .FirstOrDefault(a => a.Id == id);
}

}