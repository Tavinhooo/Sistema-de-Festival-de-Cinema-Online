using ProjetoES.API.Models;
using ProjetoES.API.Repositories;

namespace ProjetoES.API.Services;

public class FestivalService
{
    private readonly FestivalRepository _repository;

    public FestivalService(FestivalRepository repository)
    {
        _repository = repository;
    }

    public List<Festival> ObterTodosFestivais()
    {
        return _repository.ObterTodosFestivais();
    }

    public List<Festival> ObterFestivaisADecorrer()
    {
        return _repository.ObterFestivaisADecorrer();
    }

    public List<Festival> ObterFestivaisFuturos()
    {
        return _repository.ObterFestivaisFuturos();
    }

    public List<Festival> ObterFestivaisDisponiveisParaFilmes()
    {
        return _repository.ObterFestivaisDisponiveisParaFilmes();
    }

    public Festival? ObterFestivalPorId(int id)
    {
        return _repository.ObterFestivalPorId(id);
    }

    public List<Festival> FiltrarFestivais(string? nome = null, DateOnly? dataInicio = null, DateOnly? dataFim = null, string? local = null)
    {
        return _repository.FiltrarFestivais(nome, dataInicio, dataFim, local);
    }

    public void CriarFestival(Festival festival)
    {
        if (string.IsNullOrWhiteSpace(festival.Nome))
            throw new ArgumentException("O nome do festival é obrigatório.");
        if (festival.DataInicio >= festival.DataFim)
            throw new ArgumentException("A data de início deve ser anterior à data de fim.");
        if (festival.DataInicio < DateOnly.FromDateTime(DateTime.UtcNow))
            throw new ArgumentException("A data de início deve ser no futuro.");
        if (string.IsNullOrWhiteSpace(festival.Local))
            throw new ArgumentException("O local do festival é obrigatório.");
        _repository.AdicionarFestival(festival);
    }

    public void RemoverFestival(int id)
    {
        _repository.DeleteFestival(id);
    }

    public void AtualizarFestival(int id, Festival festival)
    {
        var existingFestival = _repository.ObterFestivalPorId(id)
            ?? throw new ArgumentException("Festival não encontrado.");

        if (string.IsNullOrWhiteSpace(festival.Nome))
            throw new ArgumentException("O nome do festival é obrigatório.");
        if (festival.DataInicio >= festival.DataFim)
            throw new ArgumentException("A data de início deve ser anterior à data de fim.");
        if (festival.DataInicio < DateOnly.FromDateTime(DateTime.UtcNow))
            throw new ArgumentException("A data de início deve ser no futuro.");
        if (string.IsNullOrWhiteSpace(festival.Local))
            throw new ArgumentException("O local do festival é obrigatório.");

        existingFestival.Nome = festival.Nome;
        existingFestival.DataInicio =festival.DataInicio;
        existingFestival.DataFim = festival.DataFim;
        existingFestival.Local = festival.Local;

        _repository.UpdateFestival(existingFestival);
    }
}