using ProjetoES.API.Models;
using ProjetoES.API.Repositories;

namespace ProjetoES.API.Services;

public class FilmeService
{
    private readonly FilmeRepository _repository;

    public FilmeService(FilmeRepository repository)
    {
        _repository = repository;
    }

    public List<Filme> ObterTodosFilmes()
    {
        return _repository.ObterTodosFilmes();
    }

    public Filme? ObterFilmePorId(int id)
    {
        return _repository.ObterFilmePorId(id);
    }

    public List<Filme> ObterFilmesPorFestival(int festivalId)
    {
        return _repository.ObterFilmesPorFestival(festivalId);
    }

    public void CriarFilme(Filme filme)
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

        if (filme.PrecoBilhete < 0)
        {
            throw new ArgumentException("O preço do bilhete não pode ser negativo.");
        }

        if (string.IsNullOrWhiteSpace(filme.PosterUrl))
        {
            throw new ArgumentException("A URL do poster é obrigatória.");
        }

        _repository.AdicionarFilme(filme);
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

        if (filme.PrecoBilhete < 0)
        {
            throw new ArgumentException("O preço do bilhete não pode ser negativo.");
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
        filmeExistente.PrecoBilhete = filme.PrecoBilhete;
        filmeExistente.PosterUrl = filme.PosterUrl;
        filmeExistente.FestivalId = filme.FestivalId;

        _repository.AtualizarFilme(filmeExistente);
    }

    public void EliminarFilme(int id)
    {
        _repository.EliminarFilme(id);
    }
}
