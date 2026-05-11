using ProjetoES.API.Models;
using ProjetoES.API.Repositories;
using ProjetoES.API.Interfaces;

namespace ProjetoES.API.Services;
public class AvaliacaoService : IAvaliacaoObservable
{
    private readonly AvaliacaoRepository _repository;
    private readonly List<IAvaliacaoObserver> _observers = new List<IAvaliacaoObserver>();
    public AvaliacaoService(AvaliacaoRepository repository)
    {
        _repository = repository;
    }
    public void RegistrarObserver(IAvaliacaoObserver observer) => _observers.Add(observer);
    public void RemoverObserver(IAvaliacaoObserver observer) => _observers.Remove(observer);
    public void NotificarObservers(int filmeId) => _observers.ForEach(o => o.AtualizarMediaAvaliacao(filmeId));
    public List<Avaliacao> ObterAvaliacoesPorFilme(int filmeId)
    {
        return _repository.ObterAvaliacoesPorFilme(filmeId);
    }

    public List<Avaliacao> ObterAvaliacoesPorCliente(int clienteId)
    {
        return _repository.ObterAvaliacoesPorCliente(clienteId);
    }

    public void CriarAvaliacao(Avaliacao avaliacao)
    {
        if (avaliacao.Nota < 1 || avaliacao.Nota > 5)
        {
            throw new ArgumentException("A nota deve ser entre 1 e 5.");
        }
        avaliacao.DataAvaliacao = DateTime.UtcNow;
        avaliacao.IsReportado = false;
        _repository.AdicionarAvaliacao(avaliacao);
    }

    public void EditarAvaliacao(Avaliacao avaliacao)
    {
        if (avaliacao.Nota < 1 || avaliacao.Nota > 5)
        {
            throw new ArgumentException("A nota deve ser entre 1 e 5.");
        }
        _repository.AtualizarAvaliacao(avaliacao);
    }

    public void EliminarAvaliacao(int id)
    {
        var avaliacao = _repository.ObterAvaliacaoPorId(id) ?? throw new ArgumentException("Avaliação não encontrada.");
        _repository.EliminarAvaliacao(avaliacao);
    }
    public double CalcularMediaAvaliacao(int filmeId)
    {
        var avaliacoes = _repository.ObterAvaliacoesPorFilme(filmeId);
        if (avaliacoes.Count == 0) return 0;
        return avaliacoes.Average(a => a.Nota);
    }

    public void ReportarAvaliacao(int id)
    {
        var avaliacao = _repository.ObterAvaliacaoPorId(id) ?? throw new ArgumentException("Avaliação não encontrada.");
        avaliacao.IsReportado = true;
        _repository.AtualizarAvaliacao(avaliacao);
    }
    //Moderação Admin
    public List <Avaliacao> ObterAvaliacoesReportadas()
    {
        return _repository.ObterTodasAvaliacoes()
        .Where(a => a.IsReportado)
        .ToList();
    }

    public void RejeitarReport(int id)
    {
        var avaliacao = _repository.ObterAvaliacaoPorId(id) ?? throw new ArgumentException("Avaliação não encontrada.");
        avaliacao.IsReportado = false;
        _repository.AtualizarAvaliacao(avaliacao);
    }

}