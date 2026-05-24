using ProjetoES.API.DTOs;
using ProjetoES.API.Models;
using ProjetoES.API.Repositories;
using ProjetoES.API.Pricing;

namespace ProjetoES.API.Services;

public class CarrinhoService
{
    private readonly CarrinhoRepository _repository;
    private readonly FilmeRepository _filmeRepository;

    public CarrinhoService(CarrinhoRepository repository, FilmeRepository filmeRepository)
    {
        _repository = repository;
        _filmeRepository = filmeRepository;
    }

    public List<CarrinhoResponseDTO> ObterTodosCarrinhos()
    {
        return _repository.ObterTodosCarrinhos().Select(MapearParaResponse).ToList();
    }

    public CarrinhoResponseDTO? ObterCarrinhoPorId(int id)
    {
        var carrinho = _repository.ObterCarrinhoPorId(id);
        return carrinho == null ? null : MapearParaResponse(carrinho);
    }

    public CarrinhoResponseDTO? ObterCarrinhoPorUtilizador(int utilizadorId)
    {
        var carrinho = _repository.ObterCarrinhoPorUtilizador(utilizadorId);
        return carrinho == null ? null : MapearParaResponse(carrinho);
    }

    public CarrinhoResponseDTO CriarCarrinho(CarrinhoRequestDTO dto)
    {
        if (dto.UtilizadorId <= 0)
        {
            throw new ArgumentException("O utilizador é obrigatório.");
        }

        var carrinhoExistente = _repository.ObterCarrinhoPorUtilizador(dto.UtilizadorId);
        if (carrinhoExistente != null)
        {
            return MapearParaResponse(carrinhoExistente);
        }

        var carrinho = new Carrinho
        {
            UtilizadorId = dto.UtilizadorId,
            DataCriacao = DateTime.UtcNow
        };

        _repository.CriarCarrinho(carrinho);
        return MapearParaResponse(carrinho);
    }

    public CarrinhoResponseDTO AdicionarItem(int carrinhoId, ItemCarrinhoRequestDTO dto)
    {
        if (dto.Quantidade <= 0)
            throw new ArgumentException("A quantidade tem de ser superior a zero.");

        if (dto.FestivalId <= 0)
            throw new ArgumentException("O festival é obrigatório.");

        var ePasseFestival = EPasseFestival(dto.TipoAcesso);
        if (!ePasseFestival && !dto.FilmeId.HasValue)
            throw new ArgumentException("O filme é obrigatório.");

        var carrinho = _repository.ObterCarrinhoPorId(carrinhoId);
        if (carrinho == null)
            throw new ArgumentException("Carrinho não encontrado.");

        var filmesDoFestival = _filmeRepository.ObterFilmesPorFestival(dto.FestivalId);
        if (filmesDoFestival.Count == 0)
            throw new ArgumentException("Festival sem filmes.");

        var precosBilhetes = filmesDoFestival.Select(f => f.PrecoBilhete).ToList();
        var precoBilheteFilme = dto.FilmeId.HasValue
            ? _filmeRepository.ObterPrecoBilheteFestival(dto.FilmeId.Value, dto.FestivalId)
            : 0;

        var calculator = PrecoCalculatorFactory.Criar(dto.TipoAcesso, precoBilheteFilme);
        var precoCalculado = calculator.CalcularPreco(precosBilhetes);

        var filmeIdRepresentativo = dto.FilmeId ?? filmesDoFestival.First().Id;

        var itemExistente = carrinho.Itens.FirstOrDefault(i =>
            (ePasseFestival
                ? EPasseFestival(i.TipoAcesso) && i.FestivalId == dto.FestivalId
                : i.FilmeId == dto.FilmeId) &&
            i.FestivalId == dto.FestivalId &&
            i.TipoAcesso == dto.TipoAcesso);

        if (itemExistente != null)
        {
            itemExistente.Quantidade += dto.Quantidade;
            itemExistente.PrecoUnitario = (double)precoCalculado;
        }
        else
        {
            carrinho.Itens.Add(new ItemPedido
            {
                FilmeId = filmeIdRepresentativo,
                FestivalId = dto.FestivalId,
                Quantidade = dto.Quantidade,
                PrecoUnitario = (double)precoCalculado,
                CarrinhoId = carrinho.Id,
                TipoAcesso = dto.TipoAcesso,
                Status = "Carrinho"
            });
        }

        _repository.AtualizarCarrinho(carrinho);
        return MapearParaResponse(carrinho);
    }

    public CarrinhoResponseDTO RemoverItem(int carrinhoId, int itemId)
    {
        var carrinho = _repository.ObterCarrinhoPorId(carrinhoId);
        if (carrinho == null)
        {
            throw new ArgumentException("Carrinho não encontrado.");
        }

        var item = carrinho.Itens.FirstOrDefault(i => i.Id == itemId);
        if (item == null)
        {
            throw new ArgumentException("Item não encontrado no carrinho.");
        }

        carrinho.Itens.Remove(item);
        _repository.AtualizarCarrinho(carrinho);
        return MapearParaResponse(carrinho);
    }

    public void RemoverCarrinho(int id)
    {
        _repository.RemoverCarrinho(id);
    }

    private CarrinhoResponseDTO MapearParaResponse(Carrinho carrinho)
    {
        var itens = carrinho.Itens.Select(item => new ItemCarrinhoResponseDTO
        {
            Id = item.Id,
            FilmeId = item.FilmeId,
            FilmeTitulo = EPasseFestival(item.TipoAcesso) ? item.TipoAcesso : item.Filme?.Titulo ?? string.Empty,
            FestivalId = item.FestivalId,
            FestivalNome = item.Festival?.Nome ?? string.Empty,
            Quantidade = item.Quantidade,
            TipoAcesso = item.TipoAcesso,
            PrecoOriginal = EPasseFestival(item.TipoAcesso)
                ? (double)_filmeRepository.ObterFilmesPorFestival(item.FestivalId).Sum(f => f.PrecoBilhete)
                : item.PrecoUnitario,
            PrecoUnitario = item.PrecoUnitario,
            Subtotal = item.PrecoUnitario * item.Quantidade,
            IsFestivalPass = EPasseFestival(item.TipoAcesso)
        }).ToList();

        return new CarrinhoResponseDTO
        {
            Id = carrinho.Id,
            UtilizadorId = carrinho.UtilizadorId,
            DataCriacao = carrinho.DataCriacao,
            Itens = itens,
            Total = itens.Sum(i => i.Subtotal)
        };
    }

    public CarrinhoResponseDTO AtualizarQuantidade(int carrinhoId, int itemId, int novaQuantidade)
    {
        if (novaQuantidade <= 0)
            throw new ArgumentException("A quantidade tem de ser superior a zero.");

        var carrinho = _repository.ObterCarrinhoPorId(carrinhoId);
        if (carrinho == null)
            throw new ArgumentException("Carrinho não encontrado.");

        var item = carrinho.Itens.FirstOrDefault(i => i.Id == itemId);
        if (item == null)
            throw new ArgumentException("Item não encontrado no carrinho.");

        item.Quantidade = novaQuantidade;
        _repository.AtualizarCarrinho(carrinho);
        return MapearParaResponse(carrinho);
    }

    private static bool EPasseFestival(string tipoAcesso)
    {
        if (string.IsNullOrWhiteSpace(tipoAcesso)) return false;
        var normalizado = RemoverAcentos(tipoAcesso.Trim().ToLowerInvariant());
        return normalizado == "passe completo" || normalizado == "passe diario";
    }

    private static string RemoverAcentos(string texto)
    {
        var textoNormalizado = texto.Normalize(System.Text.NormalizationForm.FormD);
        var builder = new System.Text.StringBuilder(texto.Length);

        foreach (var caractere in textoNormalizado)
        {
            var categoria = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(caractere);
            if (categoria != System.Globalization.UnicodeCategory.NonSpacingMark)
            {
                builder.Append(caractere);
            }
        }

        return builder.ToString().Normalize(System.Text.NormalizationForm.FormC);
    }
}
