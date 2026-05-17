using System.Collections;
using ProjetoES.API.Models;

namespace ProjetoES.API.Interfaces
{
    public interface IAdministradorRepository
    {
        Task<Filme> CriarFilme(Filme filme);
        Task<Filme?> ObterFilmePorId(int id);
        Task<IEnumerable<Filme>> ObterTodosFilmes();
        Task<Filme> AtualizarFilme(Filme filme);
        Task EliminarFilme(int id);

        Task<Festival> CriarFestival(Festival festival);
        Task<Festival?> ObterFestivalPorId(int id);
        Task<IEnumerable<Festival>> ObterTodosFestivais();
        Task<Festival> AtualizarFestival(Festival festival);
        Task EliminarFestival(int id);

        Task<Sessao> CriarSessao(Sessao sessao);
        Task<Sessao?> ObterSessaoPorId(int id);
        Task<IEnumerable<Sessao>> ObterTodasSessoes();
        Task<Sessao> AtualizarSessao(Sessao sessao);
        Task EliminarSessao(int id);

        Task<IEnumerable<Utilizador>> ObterTodosUtilizadores();
        Task<Utilizador?> ObterUtilizadorPorId(int id);
        Task<Utilizador> AtualizarUtilizador(Utilizador utilizador);
        Task EliminarUtilizador(int id);

        Task<IEnumerable<Avaliacao>> ObterTodasAvaliacoes();
        Task<Avaliacao?> ObterAvaliacaoPorId(int id);
        Task<Avaliacao> AprovarAvaliacao(Avaliacao avaliacao);
        Task EliminarAvaliacao(int id);

        Task<IEnumerable<Pedido>> ConsultarHistoricoGeral(DateTime? de, DateTime? ate);
        Task<IEnumerable<Pedido>> ConsultarHistoricoPorUtilizador(int utilizadorId, DateTime? de, DateTime? ate);
    }
}