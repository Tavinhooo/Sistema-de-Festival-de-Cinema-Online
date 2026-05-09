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

    public Festival? ObterFestivalPorId(int id)
    {
        return _repository.ObterFestivalPorId(id);
    }

    public void CriarFestival(Festival festival)
    {
        if (string.IsNullOrWhiteSpace(festival.Nome))
        {
            throw new ArgumentException("O nome do festival é obrigatório.");
        }
        if (festival.DataInicio >= festival.DataFim)
        {
            throw new ArgumentException("A data de início deve ser anterior à data de fim.");
        }
        if (festival.DataInicio < DateTime.Now)
        {
            throw new ArgumentException("A data de início deve ser no futuro.");
        }
        _repository.AdicionarFestival(festival);
    }
    public void RemoverFestival(int id)
    {
        _repository.DeleteFestival(id);
    }

    public void AtualizarFestival(int id, Festival festival)
    {
        var existingFestival = _repository.ObterFestivalPorId(id);
        if (existingFestival == null)
        {
            throw new ArgumentException("Festival não encontrado.");
        }
        if (string.IsNullOrWhiteSpace(festival.Nome))
        {
            throw new ArgumentException("O nome do festival é obrigatório.");
        }
        if (festival.DataInicio >= festival.DataFim)
        {
            throw new ArgumentException("A data de início deve ser anterior à data de fim.");
        }
        if (festival.DataInicio < DateTime.Now)
        {
            throw new ArgumentException("A data de início deve ser no futuro.");
        }
        // Atualiza as propriedades do festival existente
        existingFestival.Nome = festival.Nome;
        existingFestival.DataInicio = festival.DataInicio;
        existingFestival.DataFim = festival.DataFim;

        _repository.UpdateFestival(existingFestival);
    }
}