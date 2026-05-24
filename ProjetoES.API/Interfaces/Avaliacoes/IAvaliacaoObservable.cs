using ProjetoES.API.Models;
using ProjetoES.API.Repositories;

namespace ProjetoES.API.Services;
/// <summary>
/// Interface para o serviço de avaliação observável, que define os métodos para registrar, remover e notificar observadores de avaliações.
///  Esta interface é parte do padrão de design Observer,
///  onde os objetos que implementam IAvaliacaoObservable podem ser observados por objetos que implementam IAvaliacaoObserver.
///  O serviço de avaliação observável é responsável por gerenciar a lista de observadores e notificar todos os 
/// observadores registrados quando uma nova avaliação é feita para um filme específico, passando o ID do filme como parâmetro na notificação.
/// </summary>
public interface IAvaliacaoObservable
{
    void RegistrarObserver(IAvaliacaoObserver observer);
    void RemoverObserver(IAvaliacaoObserver observer);
    void NotificarObservers(int filmeId);
}