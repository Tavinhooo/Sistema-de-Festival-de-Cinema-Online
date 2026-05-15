using ProjetoES.API.DTOS;
using ProjetoES.API.Models;
using ProjetoES.API.Repositories;
using ProjetoES.API.Interfaces;
namespace ProjetoES.API.Services;

public class FilmeService : IAvaliacaoObserver
{
    private readonly FilmeRepository _repository;
    private readonly AvaliacaoService _avaliacaoService;

    public FilmeService(FilmeRepository repository, AvaliacaoService avaliacaoService)
    {
        _repository = repository;
        _avaliacaoService = avaliacaoService;
        _avaliacaoService.RegistrarObserver(this);
    }

    public void AtualizarMediaAvaliacao(int filmeId)
    {
        var filme = _repository.ObterFilmePorId(filmeId);
        if (filme == null) return;
        filme.MediaAvaliacao = _avaliacaoService.CalcularMediaAvaliacao(filmeId);
        _repository.AtualizarFilme(filme);
    }
    public List<Filme> ObterTodosFilmes()
    {
        return _repository.ObterTodosFilmes();
    }

    public Filme? ObterFilmePorId(int id)
    {
        return _repository.ObterFilmePorId(id);
    }

    public List<FilmeFestivalDTO> ObterFilmesPorFestival(int festivalId)
    {
        return _repository.ObterFilmesPorFestival(festivalId);
    }

    public FilmeFestivalDTO? ObterFilmePorFestival(int filmeId, int festivalId)
    {
        return _repository.ObterFilmePorFestival(filmeId, festivalId);
    }

    public void CriarFilme(Filme filme, int? festivalId = null, decimal? precoBilhete = null)
    {
        if (string.IsNullOrWhiteSpace(filme.Titulo))
        {
            throw new ArgumentException("O título do filme é obrigatório.");
        }

        if (filme.Ano <= 0 || filme.Ano > DateTime.Now.Year)
        {
            throw new ArgumentException("O ano do filme deve ser válido.");
        }

        if (filme.DuracaoMinutos <= 0)
        {
            throw new ArgumentException("A duração do filme deve ser superior a 0 minutos.");
        }

        if (string.IsNullOrWhiteSpace(filme.PosterUrl))
        {
            throw new ArgumentException("A URL do poster é obrigatória.");
        }

        _repository.AdicionarFilme(filme, festivalId, precoBilhete);
    }

    public void VincularFilmeAoFestival(int filmeId, int festivalId, decimal precoBilhete)
    {
        _repository.VincularFilmeAoFestival(filmeId, festivalId, precoBilhete);
    }

    public void DesvincularFilmeDeFestival(int filmeId, int festivalId)
    {
        _repository.DesvincularFilmeDeFestival(filmeId, festivalId);
    }

    public void AtualizarFilme(int id, Filme filme)
    {
        var filmeExistente = _repository.ObterFilmePorId(id);
        if (filmeExistente == null)
        {
            throw new ArgumentException("Filme não encontrado.");
        }

        if (string.IsNullOrWhiteSpace(filme.Titulo))
        {
            throw new ArgumentException("O título do filme é obrigatório.");
        }

        if (filme.Ano <= 0 || filme.Ano > DateTime.Now.Year)
        {
            throw new ArgumentException("O ano do filme deve ser válido.");
        }

        if (filme.DuracaoMinutos <= 0)
        {
            throw new ArgumentException("A duração do filme deve ser superior a 0 minutos.");
        }

        if (string.IsNullOrWhiteSpace(filme.PosterUrl))
        {
            throw new ArgumentException("A URL do poster é obrigatória.");
        }

        // Atualiza as propriedades
        filmeExistente.Titulo = filme.Titulo;
        filmeExistente.Sinopse = filme.Sinopse;
        filmeExistente.Genero = filme.Genero;
        filmeExistente.Ano = filme.Ano;
        filmeExistente.DuracaoMinutos = filme.DuracaoMinutos;
        filmeExistente.PosterUrl = filme.PosterUrl;
        filmeExistente.TrailerUrl = filme.TrailerUrl;
        filmeExistente.Realizador = filme.Realizador;
        filmeExistente.Elenco = filme.Elenco;

        _repository.AtualizarFilme(filmeExistente);
    }

    public void EliminarFilme(int id)
    {
        _repository.EliminarFilme(id);
    }
}
