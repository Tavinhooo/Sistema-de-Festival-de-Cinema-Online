using ProjetoES.API.Models;
using ProjetoES.API.Repositories;

namespace ProjetoES.API.Services;
public class AvaliacaoService
{
    private readonly AvaliacaoRepository _repository;

    public AvaliacaoService(AvaliacaoRepository repository)
    {
        _repository = repository;
    }

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
}