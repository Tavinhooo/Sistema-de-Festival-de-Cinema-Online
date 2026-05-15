using ProjetoES.API.Data;
using ProjetoES.API.DTOS;
using ProjetoES.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ProjetoES.API.Repositories;

public class FilmeRepository
{
    private readonly AppDbContext _context;

    public FilmeRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<Filme> ObterTodosFilmes()
    {
        return _context.Filmes.ToList();
    }

    public Filme? ObterFilmePorId(int id)
    {
        return _context.Filmes.FirstOrDefault(f => f.Id == id);
    }

    public List<FilmeFestivalDTO> ObterFilmesPorFestival(int festivalId)
    {
        return _context.FestivalFilmes
            .Include(ff => ff.Filme)
            .Include(ff => ff.Festival)
            .Where(ff => ff.FestivalId == festivalId)
            .Select(ff => new FilmeFestivalDTO
            {
                Id = ff.Filme.Id,
                Titulo = ff.Filme.Titulo,
                Sinopse = ff.Filme.Sinopse,
                Genero = ff.Filme.Genero,
                Ano = ff.Filme.Ano,
                DuracaoMinutos = ff.Filme.DuracaoMinutos,
                MediaAvaliacao = ff.Filme.MediaAvaliacao,
                PosterUrl = ff.Filme.PosterUrl,
                TrailerUrl = ff.Filme.TrailerUrl,
                FestivalId = ff.FestivalId,
                FestivalNome = ff.Festival.Nome,
                PrecoBilhete = ff.PrecoBilhete,
                Realizador = ff.Filme.Realizador,
                Elenco = ff.Filme.Elenco
            })
            .ToList();
    }

    public void AdicionarFilme(Filme filme, int? festivalId = null, decimal? precoBilhete = null)
    {
        if (festivalId.HasValue)
        {
            var festival = _context.Festivais.Find(festivalId.Value);
            if (festival == null)
            {
                throw new ArgumentException("Festival não encontrado.");
            }

            var preco = precoBilhete ?? 0m;
            if (preco <= 0)
            {
                throw new ArgumentException("O preço do bilhete do festival é obrigatório.");
            }

            _context.Filmes.Add(filme);
            _context.SaveChanges();

            _context.FestivalFilmes.Add(new FestivalFilme
            {
                FestivalId = festival.Id,
                FilmeId = filme.Id,
                PrecoBilhete = preco
            });

            _context.SaveChanges();
            return;
        }

        _context.Filmes.Add(filme);
        _context.SaveChanges();
    }

    public void VincularFilmeAoFestival(int filmeId, int festivalId, decimal precoBilhete)
    {
        var filme = _context.Filmes.FirstOrDefault(f => f.Id == filmeId);

        if (filme == null)
        {
            throw new ArgumentException("Filme não encontrado.");
        }

        var festival = _context.Festivais.Find(festivalId);
        if (festival == null)
        {
            throw new ArgumentException("Festival não encontrado.");
        }

        if (precoBilhete <= 0)
        {
            throw new ArgumentException("O preço do bilhete do festival é obrigatório.");
        }

        var ligacaoExistente = _context.FestivalFilmes.FirstOrDefault(ff => ff.FestivalId == festivalId && ff.FilmeId == filmeId);
        if (ligacaoExistente == null)
        {
            _context.FestivalFilmes.Add(new FestivalFilme
            {
                FestivalId = festivalId,
                FilmeId = filmeId,
                PrecoBilhete = precoBilhete
            });
            _context.SaveChanges();
            return;
        }

        ligacaoExistente.PrecoBilhete = precoBilhete;
        _context.SaveChanges();
    }

    public void DesvincularFilmeDeFestival(int filmeId, int festivalId)
    {
        var ligacao = _context.FestivalFilmes.FirstOrDefault(ff => ff.FestivalId == festivalId && ff.FilmeId == filmeId);
        if (ligacao == null)
        {
            return;
        }

        _context.FestivalFilmes.Remove(ligacao);
        _context.SaveChanges();
    }

    public decimal ObterPrecoBilheteFestival(int filmeId, int festivalId)
    {
        var ligacao = _context.FestivalFilmes.FirstOrDefault(ff => ff.FilmeId == filmeId && ff.FestivalId == festivalId);
        if (ligacao == null)
        {
            throw new ArgumentException("Filme não associado ao festival.");
        }

        return ligacao.PrecoBilhete;
    }

    public FilmeFestivalDTO? ObterFilmePorFestival(int filmeId, int festivalId)
    {
        return _context.FestivalFilmes
            .Include(ff => ff.Filme)
            .Include(ff => ff.Festival)
            .Where(ff => ff.FilmeId == filmeId && ff.FestivalId == festivalId)
            .Select(ff => new FilmeFestivalDTO
            {
                Id = ff.Filme.Id,
                Titulo = ff.Filme.Titulo,
                Sinopse = ff.Filme.Sinopse,
                Genero = ff.Filme.Genero,
                Ano = ff.Filme.Ano,
                DuracaoMinutos = ff.Filme.DuracaoMinutos,
                MediaAvaliacao = ff.Filme.MediaAvaliacao,
                PosterUrl = ff.Filme.PosterUrl,
                TrailerUrl = ff.Filme.TrailerUrl,
                FestivalId = ff.FestivalId,
                FestivalNome = ff.Festival.Nome,
                PrecoBilhete = ff.PrecoBilhete,
                Realizador = ff.Filme.Realizador,
                Elenco = ff.Filme.Elenco
            })
            .FirstOrDefault();
    }

    public void AtualizarFilme(Filme filme)
    {
        _context.Filmes.Update(filme);
        _context.SaveChanges();
    }

    public void EliminarFilme(int id)
    {
        var filme = _context.Filmes.Find(id);
        if (filme != null)
        {
            _context.Filmes.Remove(filme);
            _context.SaveChanges();
        }
    }
}
