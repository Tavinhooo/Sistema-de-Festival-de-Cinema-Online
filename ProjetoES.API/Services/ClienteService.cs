using ProjetoES.API.DTOS;
using ProjetoES.API.Models;
using ProjetoES.API.Repositories;

namespace ProjetoES.API.Services;

public class ClienteService
{
    private readonly ClienteRepository _repo;
    private readonly AuthService _authService;

    public ClienteService(ClienteRepository repo, AuthService authService)
    {
        _repo = repo;
        _authService = authService;
    }

    // RF07 / RU10 - Histórico de compras
    public List<HistoricoComprasDTO> ObterHistoricoCompras(int clienteId)
    {
        var pedidos = _repo.ObterHistoricoCompras(clienteId);
        return pedidos.Select(p => new HistoricoComprasDTO
        {
            PedidoId = p.Id,
            DataPedido = p.DataPedido,
            DataPagamento = p.DataPagamento,
            PrecoTotal = p.PrecoTotal,
            Estado = p.Estado.ToString(),
            Itens = p.Itens.Select(i => new ItemPedidoDTO
            {
                FilmeId = i.FilmeId,
                FilmeTitulo = i.Filme?.Titulo,
                TipoAcesso = i.TipoAcesso,
                Quantidade = i.Quantidade,
                PrecoUnitario = i.PrecoUnitario
            }).ToList()
        }).ToList();
    }

    // RU09 - Acessos reservados
    public List<AcessoResponseDTO> ObterAcessos(int clienteId)
    {
        var acessos = _repo.ObterAcessos(clienteId);
        return acessos.Select(a => new AcessoResponseDTO
        {
            Id = a.Id,
            FilmeId = a.FilmeId,
            FilmeTitulo = a.Filme?.Titulo,
            TipoAcesso = a.TipoAcesso,
            DataAquisicao = a.DataAquisicao,
            DataValidade = a.DataValidade,
            Estado = a.Estado.ToString()
        }).ToList();
    }

    // RF13 / RU08 - Votar e comentar num filme
    public AvaliacaoResponseDTO CriarAvaliacao(int clienteId, CriarAvaliacaoDTO dto)
    {
        if (_repo.JaAvaliou(clienteId, dto.FilmeId))
            throw new ArgumentException("Já avaliaste este filme.");

        var avaliacao = new Avaliacao
        {
            ClienteId = clienteId,
            FilmeId = dto.FilmeId,
            Classificacao = dto.Classificacao,
            Comentario = dto.Comentario,
            DataAvaliacao = DateTime.UtcNow
        };

        var criada = _repo.CriarAvaliacao(avaliacao);

        return new AvaliacaoResponseDTO
        {
            Id = criada.Id,
            FilmeId = criada.FilmeId,
            Classificacao = criada.Classificacao,
            Comentario = criada.Comentario,
            DataAvaliacao = criada.DataAvaliacao
        };
    }

    // RU08 - Ver avaliações feitas pelo Cliente
    public List<AvaliacaoResponseDTO> ObterAvaliacoes(int clienteId)
    {
        var avaliacoes = _repo.ObterAvaliacoesDoCliente(clienteId);
        return avaliacoes.Select(a => new AvaliacaoResponseDTO
        {
            Id = a.Id,
            FilmeId = a.FilmeId,
            FilmeTitulo = a.Filme?.Titulo,
            Classificacao = a.Classificacao,
            Comentario = a.Comentario,
            DataAvaliacao = a.DataAvaliacao
        }).ToList();
    }

    // RF04 - Promover Membro a Cliente e emitir novo JWT
    public AuthResponseDTO PromoverParaCliente(int utilizadorId)
    {
        var utilizador = _repo.ObterPorId(utilizadorId)
            ?? throw new ArgumentException("Utilizador não encontrado.");

        if (utilizador.Tipo == TipoUtilizador.Cliente)
            throw new ArgumentException("Utilizador já é Cliente.");

        _repo.PromoverParaCliente(utilizador);

        // Emite novo JWT com role "Cliente"
        return _authService.GenerateTokenPublico(utilizador);
    }
}