using ProjetoES.API.Models;
using ProjetoES.API.Repositories;

namespace ProjetoES.API.Services;
//Quem recebe - Filme
public interface IAvaliacaoObserver
{
    void AtualizarMediaAvaliacao(int filmeId);
}