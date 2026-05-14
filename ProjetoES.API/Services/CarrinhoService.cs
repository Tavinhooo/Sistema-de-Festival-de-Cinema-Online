using ProjetoES.API.DTOS;
using ProjetoES.API.Models;
using ProjetoES.API.Repositories;

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
        {
            throw new ArgumentException("A quantidade tem de ser superior a zero.");
        }

        if (dto.FestivalId <= 0)
        {
            throw new ArgumentException("O festival é obrigatório.");
        }

        var carrinho = _repository.ObterCarrinhoPorId(carrinhoId);
        if (carrinho == null)
        {
            throw new ArgumentException("Carrinho não encontrado.");
        }

        var filme = _filmeRepository.ObterFilmePorId(dto.FilmeId);
        if (filme == null)
        {
            throw new ArgumentException("Filme inválido.");
        }

        var precoFestival = _filmeRepository.ObterPrecoBilheteFestival(dto.FilmeId, dto.FestivalId);

        var itemExistente = carrinho.Itens.FirstOrDefault(i => i.FilmeId == dto.FilmeId && i.FestivalId == dto.FestivalId && i.TipoAcesso == dto.TipoAcesso);
        if (itemExistente != null)
        {
            itemExistente.Quantidade += dto.Quantidade;
            itemExistente.PrecoUnitario = (double)precoFestival;
            itemExistente.TipoAcesso = dto.TipoAcesso;
        }
        else
        {
            carrinho.Itens.Add(new ItemPedido
            {
                FilmeId = dto.FilmeId,
                FestivalId = dto.FestivalId,
                Quantidade = dto.Quantidade,
                PrecoUnitario = (double)precoFestival,
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
            FilmeTitulo = item.Filme?.Titulo ?? string.Empty,
            FestivalId = item.FestivalId,
            Quantidade = item.Quantidade,
            TipoAcesso = item.TipoAcesso,
            PrecoUnitario = item.PrecoUnitario,
            Subtotal = item.PrecoUnitario * item.Quantidade
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
}