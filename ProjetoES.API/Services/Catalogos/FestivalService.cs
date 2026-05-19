using ProjetoES.API.Models;
using ProjetoES.API.Repositories;

namespace ProjetoES.API.Services;

public class FestivalService
{
    private readonly FestivalRepository _festivalRepository;
    private readonly FilmeRepository _filmeRepository;

    public FestivalService(FestivalRepository festivalRepository, FilmeRepository filmeRepository)
    {
        _festivalRepository = festivalRepository;
        _filmeRepository = filmeRepository;
    }

    public List<Festival> ObterTodosFestivais()
    {
        return _festivalRepository.ObterTodosFestivais();
    }

    public List<Festival> ObterFestivaisADecorrer()
    {
        return _festivalRepository.ObterFestivaisADecorrer();
    }

    public List<Festival> ObterFestivaisFuturos()
    {
        return _festivalRepository.ObterFestivaisFuturos();
    }

    public List<Festival> ObterFestivaisDisponiveisParaFilmes()
    {
        return _festivalRepository.ObterFestivaisDisponiveisParaFilmes();
    }

    public Festival? ObterFestivalPorId(int id)
    {
        return _festivalRepository.ObterFestivalPorId(id);
    }

    public void CriarFestival(Festival festival)
    {
        if (string.IsNullOrWhiteSpace(festival.Nome))
            throw new ArgumentException("O nome do festival é obrigatório.");

        _festivalRepository.AdicionarFestival(festival);
    }

    public void AtualizarFestival(int id, Festival festival)
    {
        var existente = _festivalRepository.ObterFestivalPorId(id);
        if (existente == null)
            throw new ArgumentException("Festival não encontrado para atualização.");

        festival.Id = id;
        _festivalRepository.UpdateFestival(festival);
    }

    public void RemoverFestival(int id)
    {
        _festivalRepository.DeleteFestival(id);
    }

    public List<Festival> FiltrarFestivais(string? nome, string? descricao, DateOnly? dataInicio, DateOnly? dataFim, string? local)
    {
        return _festivalRepository.FiltrarFestivais(nome, descricao, dataInicio, dataFim, local);
    }

    public void AssociarFilmeAoFestival(int festivalId, int filmeId)
    {
        var filme = _filmeRepository.ObterFilmePorId(filmeId);
        if (filme == null)
            throw new ArgumentException("O filme selecionado não existe na base de dados.");
        _festivalRepository.AssociarFilmeAoFestival(festivalId, filme);
    }
}