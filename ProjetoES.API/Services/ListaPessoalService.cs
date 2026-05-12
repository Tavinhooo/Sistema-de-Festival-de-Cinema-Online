using ProjetoES.API.Interfaces;
using ProjetoES.API.Models;
using ProjetoES.API.Factories;
using ProjetoES.API.Data;

namespace ProjetoES.API.Services;

public class ListaPessoalService : IListaPessoalService
{
    private readonly IListaPessoalRepository _repository;
    private readonly AppDbContext _context;
    public ListaPessoalService(IListaPessoalRepository repository, AppDbContext context)
    {
        _repository = repository;
        _context = context;
    }
    public ListaPessoal CriarLista(int membroId, TipoLista tipo)
    {
        var utilizador = _context.Utilizadores.Find(membroId)
            ?? throw new ArgumentException("Utilizador não encontrado");

        var lista = new ListaPessoal
        {
            MembroId = membroId,
            Tipo = tipo,
            Filmes = new List<Filme>()
        };
        _repository.Adicionar(lista);
        _context.SaveChanges();
        return lista;
    }
    public IEnumerable<ListaPessoal> ObterPorMembro(int membroId) => _repository.ObterPorMembro(membroId);
    public ListaPessoal? ObterLista(int listaId) => _repository.ObterPorId(listaId);
    public void AdicionarFilme(int listaId, int filmeId)
    {
        var lista = GetOrThrow(listaId);
        var strategy = ListaPessoalFactory.Criar(lista.Tipo);
        if (lista.Filmes.Count >= strategy.LimiteMaximo)
        {
            throw new InvalidOperationException($"A lista {strategy.NomeLista} atingiu o limite máximo de filmes.");
        }
        var filme = _context.Filmes.Find(filmeId) ?? throw new ArgumentException("Filme não encontrado");

        lista.Filmes.Add(filme);
        _repository.Atualizar(lista);
        _context.SaveChanges();
    }
    public void RemoverFilme(int listaId, int filmeId)
    {
        var lista = GetOrThrow(listaId);
        var filme = lista.Filmes.FirstOrDefault(f => f.Id == filmeId);
        if (filme == null)
        {
            throw new ArgumentException("Filme não encontrado na lista.");
        }
        lista.Filmes.Remove(filme);
        _repository.Atualizar(lista);
        _context.SaveChanges();
    }
    public void MudarTipoLista(int listaId, TipoLista novoTipo)
    {
        var lista = GetOrThrow(listaId);
        lista.Tipo = novoTipo;
        _repository.Atualizar(lista);
        _context.SaveChanges();
    }
    public void RemoverLista(int listaId)
    {
        var lista = GetOrThrow(listaId);
        _repository.Remover(lista.Id);
        _context.SaveChanges();
    }

    public ListaPessoal? GetOrThrow(int id) => _repository.ObterPorId(id) ?? throw new ArgumentException("Lista não encontrada");
}