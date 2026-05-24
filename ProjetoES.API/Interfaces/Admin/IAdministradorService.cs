using ProjetoES.API.Models;
using ProjetoES.API.DTOs;

namespace ProjetoES.API.Interfaces
{
    /// <summary>
    /// Interface para o serviço de administrador, que define os métodos para a gestão de filmes,
    /// festivais, sessões, utilizadores, avaliações e histórico de pedidos.
    /// </summary>
    public interface IAdministradorService
    {
        Task<FilmeDTO> GerirFilme(int adminId, FilmeDTO filmeDto);
        Task EliminarFilme(int adminId, int filmeId);

        Task<FestivalDTO> GerirFestival(int adminId, FestivalDTO festivalDto);
        Task EliminarFestival(int adminId, int festivalId);

        Task<SessaoDTO> GerirSessao(int adminId, SessaoDTO sessaoDto);
        Task AtualizarSessao(int adminId, SessaoDTO sessaoDto);
        Task CancelarSessao(int adminId, int sessaoId);

        Task<IEnumerable<UtilizadorDTO>> ListarUtilizadores(int adminId);
        Task<UtilizadorDTO> AlterarTipoUtilizador(int adminId, int utilizadorId, TipoUtilizador novoTipo);
        Task EliminarUtilizador(int adminId, int utilizadorId);

        Task<IEnumerable<AvaliacaoDTO>> ListarAvaliacoes(int adminId);
        Task<AvaliacaoDTO> AprovarAvaliacao(int adminId, int avaliacaoId);
        Task EliminarAvaliacao(int adminId, int avaliacaoId);

        Task<IEnumerable<PedidoDTO>> ConsultarHistoricoGeral(int adminId, DateTime? de, DateTime? ate);
        Task<IEnumerable<PedidoDTO>> ConsultarHistoricoPorUtilizador(int adminId, int utilizadorId, DateTime? de, DateTime? ate);
    }
}
