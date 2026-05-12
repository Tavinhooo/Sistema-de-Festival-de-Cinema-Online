using ProjetoES.API.DTOS;
using ProjetoES.API.Models;
using ProjetoES.API.Repositories;

namespace ProjetoES.API.Services;

public class MembroService
{
    private readonly MembroRepository _repo;

    public MembroService(MembroRepository repo)
    {
        _repo = repo;
    }

    // RF05.1 / RU05 - Logout: marca IsLogged = false
    public void RealizarLogout(int membroId)
    {
        var membro = ObterMembroOuErro(membroId);
        membro.IsLogged = false;
        _repo.AtualizarMembro(membro);
    }

    // Consultar perfil próprio
    public MembroPerfilDTO ObterPerfil(int membroId)
    {
        var membro = ObterMembroOuErro(membroId);
        return MapearParaDTO(membro);
    }

    // Atualizar nome
    public MembroPerfilDTO AtualizarPerfil(int membroId, AtualizarPerfilDTO dto)
    {
        var membro = ObterMembroOuErro(membroId);

        if (!string.IsNullOrWhiteSpace(dto.PrimeiroNome))
            membro.PrimeiroNome = dto.PrimeiroNome;

        if (!string.IsNullOrWhiteSpace(dto.UltimoNome))
            membro.UltimoNome = dto.UltimoNome;

        _repo.AtualizarMembro(membro);
        return MapearParaDTO(membro);
    }

    // RU06 - Adicionar/atualizar morada de faturação
    public MembroPerfilDTO AtualizarMorada(int membroId, AtualizarMoradaDTO dto)
    {
        if (dto == null || dto.Morada == null)
            throw new ArgumentException("Morada de faturação não pode ser vazia.");

        var m = dto.Morada;
        if (string.IsNullOrWhiteSpace(m.MoradaFaturacao) || string.IsNullOrWhiteSpace(m.CodigoPostal) || string.IsNullOrWhiteSpace(m.Localidade))
            throw new ArgumentException("Morada inválida: morada, código postal e localidade são obrigatórios.");

        var membro = ObterMembroOuErro(membroId);
        membro.MoradaFaturacao = new Morada
        {
            NomeDestinatario = m.NomeDestinatario ?? string.Empty,
            MoradaFaturacao = m.MoradaFaturacao ?? string.Empty,
            CodigoPostal = m.CodigoPostal ?? string.Empty,
            Localidade = m.Localidade ?? string.Empty,
            Pais = m.Pais ?? string.Empty
        };
        _repo.AtualizarMembro(membro);
        return MapearParaDTO(membro);
    }

    // RU07 (prep) - Selecionar método de pagamento
    public MembroPerfilDTO AtualizarMetodoPagamento(int membroId, AtualizarMetodoPagamentoDTO dto)
    {
        var metodosValidos = new[] { "MBWay", "Cartao", "Multibanco" };
        if (!metodosValidos.Contains(dto.MetodoPagamento))
            throw new ArgumentException($"Método de pagamento inválido. Opções: {string.Join(", ", metodosValidos)}");

        var membro = ObterMembroOuErro(membroId);
        membro.MetodoPagamento = dto.MetodoPagamento;
        _repo.AtualizarMembro(membro);
        return MapearParaDTO(membro);
    }

    // --- Helpers ---

    private Utilizador ObterMembroOuErro(int id)
    {
        var membro = _repo.ObterPorId(id);
        if (membro == null)
            throw new ArgumentException("Utilizador não encontrado.");
        return membro;
    }

    private static MembroPerfilDTO MapearParaDTO(Utilizador u) => new()
    {
        Id = u.Id,
        PrimeiroNome = u.PrimeiroNome,
        UltimoNome = u.UltimoNome,
        Email = u.Email,
        Tipo = u.Tipo.ToString(),
        MetodoPagamento = u.MetodoPagamento,
        MoradaFaturacao = u.MoradaFaturacao == null ? null : new MoradaDTO
        {
            NomeDestinatario = u.MoradaFaturacao.NomeDestinatario,
            MoradaFaturacao = u.MoradaFaturacao.MoradaFaturacao,
            CodigoPostal = u.MoradaFaturacao.CodigoPostal,
            Localidade = u.MoradaFaturacao.Localidade,
            Pais = u.MoradaFaturacao.Pais
        }
    };
}