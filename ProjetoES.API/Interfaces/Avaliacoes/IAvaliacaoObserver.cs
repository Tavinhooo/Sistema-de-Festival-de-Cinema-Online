using ProjetoES.API.Models;
using ProjetoES.API.Repositories;

namespace ProjetoES.API.Services;
/// <summary>
/// Interface para o serviço de avaliação observável, que define os métodos para registrar, remover e notificar observadores de avaliações.
/// Esta interface é parte do padrão de design Observer,
public interface IAvaliacaoObserver
{
    void AtualizarMediaAvaliacao(int filmeId);
}