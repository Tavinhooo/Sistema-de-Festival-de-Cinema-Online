using ProjetoES.API.DTOS;
using ProjetoES.API.Models;
using ProjetoES.API.Repositories;

namespace ProjetoES.API.Services;

public class SessaoService
{
    private readonly SessaoRepository _repository;
    private readonly FestivalRepository _festivalRepository;
    private readonly FilmeRepository _filmeRepository;

    public SessaoService(SessaoRepository repository, FestivalRepository festivalRepository, FilmeRepository filmeRepository)
    {
        _repository = repository;
        _festivalRepository = festivalRepository;
        _filmeRepository = filmeRepository;
    }

    public List<SessaoResponseDTO> ObterTodasSessoes()
    {
        return _repository.ObterTodasSessoes().Select(MapearParaResponse).ToList();
    }

    public SessaoResponseDTO? ObterSessaoPorId(int id)
    {
        var sessao = _repository.ObterSessaoPorId(id);
        return sessao == null ? null : MapearParaResponse(sessao);
    }

    public List<SessaoResponseDTO> ObterSessoesPorFestival(int festivalId)
    {
        return _repository.ObterSessoesPorFestival(festivalId).Select(MapearParaResponse).ToList();
    }

    public List<SessaoResponseDTO> ObterSessoesPorFilme(int filmeId)
    {
        return _repository.ObterSessoesPorFilme(filmeId).Select(MapearParaResponse).ToList();
    }

    public SessaoResponseDTO CriarSessao(SessaoRequestDTO dto)
    {
        var sessao = new Sessao
        {
            FestivalId = dto.FestivalId,
            FilmeId = dto.FilmeId,
            DataInicio = dto.DataInicio,
            DataFim = dto.DataFim,
            Sala = dto.Sala,
            Tipo = dto.Tipo
        };

        ValidarSessao(sessao);
        _repository.AdicionarSessao(sessao);
        return MapearParaResponse(sessao);
    }

    public SessaoResponseDTO AtualizarSessao(int id, SessaoRequestDTO dto)
    {
        var sessaoExistente = _repository.ObterSessaoPorId(id);
        if (sessaoExistente == null)
        {
            throw new ArgumentException("Sessão não encontrada.");
        }

        var sessao = new Sessao
        {
            FestivalId = dto.FestivalId,
            FilmeId = dto.FilmeId,
            DataInicio = dto.DataInicio,
            DataFim = dto.DataFim,
            Sala = dto.Sala,
            Tipo = dto.Tipo
        };

        ValidarSessao(sessao);

        sessaoExistente.FestivalId = sessao.FestivalId;
        sessaoExistente.FilmeId = sessao.FilmeId;
        sessaoExistente.DataInicio = sessao.DataInicio;
        sessaoExistente.DataFim = sessao.DataFim;
        sessaoExistente.Sala = sessao.Sala;
        sessaoExistente.Tipo = sessao.Tipo;

        _repository.AtualizarSessao(sessaoExistente);
        return MapearParaResponse(sessaoExistente);
    }

    public void EliminarSessao(int id)
    {
        _repository.EliminarSessao(id);
    }

    private void ValidarSessao(Sessao sessao)
    {
        if (sessao.FestivalId <= 0 || _festivalRepository.ObterFestivalPorId(sessao.FestivalId) == null)
        {
            throw new ArgumentException("Festival inválido.");
        }

        if (sessao.FilmeId <= 0 || _filmeRepository.ObterFilmePorId(sessao.FilmeId) == null)
        {
            throw new ArgumentException("Filme inválido.");
        }

        if (sessao.DataInicio >= sessao.DataFim)
        {
            throw new ArgumentException("A data de início deve ser anterior à data de fim.");
        }

        if (string.IsNullOrWhiteSpace(sessao.Sala))
        {
            throw new ArgumentException("A sala é obrigatória.");
        }
    }

    private SessaoResponseDTO MapearParaResponse(Sessao sessao)
    {
        return new SessaoResponseDTO
        {
            Id = sessao.Id,
            FestivalId = sessao.FestivalId,
            FestivalNome = sessao.Festival?.Nome ?? string.Empty,
            FilmeId = sessao.FilmeId,
            FilmeTitulo = sessao.Filme?.Titulo ?? string.Empty,
            DataInicio = sessao.DataInicio,
            DataFim = sessao.DataFim,
            Sala = sessao.Sala,
            Tipo = sessao.Tipo
        };
    }
}