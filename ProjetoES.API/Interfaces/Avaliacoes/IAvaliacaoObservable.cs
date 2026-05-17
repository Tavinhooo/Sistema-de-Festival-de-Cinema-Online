using ProjetoES.API.Models;
using ProjetoES.API.Repositories;

namespace ProjetoES.API.Services;
//Quem notifica - Avaliação
public interface IAvaliacaoObservable
{
    void RegistrarObserver(IAvaliacaoObserver observer);
    void RemoverObserver(IAvaliacaoObserver observer);
    void NotificarObservers(int filmeId);
}