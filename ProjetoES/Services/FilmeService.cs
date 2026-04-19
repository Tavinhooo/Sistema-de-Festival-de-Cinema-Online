using Microsoft.EntityFrameworkCore;
using ProjetoES.Data;
using ProjetoES.Interfaces;
using ProjetoES.Models;

namespace ProjetoES.Services
{
    // A classe implementa a Interface IFilmeService
    public class FilmeService : IFilmeService
    {
        private readonly ApplicationDbContext _context;

        // Injeção de Dependências (Boa prática das tuas aulas!)
        public FilmeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarFilmeAsync(Filme filme)
        {
            _context.Filmes.Add(filme);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Filme>> ObterTodosFilmesAsync()
        {
            // Ordenar para mostrar os mais recentes primeiro
            return await _context.Filmes.OrderByDescending(f => f.Ano).ToListAsync();
        }
    }
}