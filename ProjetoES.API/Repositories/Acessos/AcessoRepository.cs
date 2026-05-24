using Microsoft.EntityFrameworkCore;
using ProjetoES.API.DTOs;
using ProjetoES.API.Data;
using ProjetoES.API.Models;

namespace ProjetoES.API.Repositories;

public class AcessoRepository
{
    private readonly AppDbContext _context;

    public AcessoRepository(AppDbContext context)
    {
        _context = context;
    }

    public void CriarAcessos(IEnumerable<Acesso> acessos)
    {
        _context.Acessos.AddRange(acessos);
        _context.SaveChanges();
    }
   
    public bool VerificarAcessoFilmeNoFestival(int clienteId, int filmeId, int festivalId)
    {
        bool temBilheteFilme = _context.Acessos.Any(a =>
            a.ClienteId == clienteId &&
            a.FilmeId == filmeId &&
            a.FestivalId == festivalId &&
            a.Estado == EstadoAcesso.Ativo &&
            (a.TipoAcesso == "Bilhete de Sessão" || a.TipoAcesso == "Aluguer Digital"));

        if (temBilheteFilme) return true;

        bool filmePertenceAoFestival = _context.FestivalFilmes
            .Any(ff => ff.FestivalId == festivalId && ff.FilmeId == filmeId);

        if (filmePertenceAoFestival)
        {
            bool temPasseFestival = _context.Acessos.Any(a =>
                a.ClienteId == clienteId &&
                a.Estado == EstadoAcesso.Ativo &&
                a.FestivalId == festivalId &&
                (a.TipoAcesso.ToLower() == "passe completo" || a.TipoAcesso.ToLower() == "passe diário"));

            if (temPasseFestival) return true;
        }
        var acessosCliente = _context.Acessos
    .Where(a => a.ClienteId == clienteId)
    .Select(a => new { a.FilmeId, a.FestivalId, a.TipoAcesso, a.Estado })
    .ToList();
Console.WriteLine($"Acessos: {System.Text.Json.JsonSerializer.Serialize(acessosCliente)}");
        return false;
    }
    
    public List<AcessoResponseDTO> ObterFilmesComAcesso(int clienteId)
    {
        var acessos = _context.Acessos
            .Where(a => a.ClienteId == clienteId && a.Estado == EstadoAcesso.Ativo)
            .Include(a => a.Filme)
            .OrderByDescending(a => a.DataAquisicao)
            .ToList();

        return acessos
            .GroupBy(a => new { a.FilmeId, a.FestivalId })
            .Select(g => g.First())
            .Select(a => new AcessoResponseDTO
            {
                Id = a.Id,
                FilmeId = a.FilmeId,
                FestivalId = a.FestivalId,
                FilmeTitulo = a.Filme.Titulo,
                PosterUrl = a.Filme.PosterUrl,
                TipoAcesso = a.TipoAcesso,
                DataAquisicao = a.DataAquisicao,
                DataValidade = a.DataValidade,
                Estado = a.Estado.ToString()
            })
            .OrderBy(a => a.FilmeTitulo)
            .ToList();
    }
}