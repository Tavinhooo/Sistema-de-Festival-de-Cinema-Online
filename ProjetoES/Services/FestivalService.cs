using Microsoft.EntityFrameworkCore;
using ProjetoES.Data;
using ProjetoES.Interfaces;
using ProjetoES.Models;

namespace ProjetoES.Services
{
    public class FestivalService : IFestivalService
    {
        private readonly ApplicationDbContext _context;

        public FestivalService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarFestivalAsync(Festival festival)
        {
            _context.Festivais.Add(festival);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Festival>> ObterTodosFestivaisAsync()
        {
            // Ordenar pela data de início mais próxima
            return await _context.Festivais.OrderBy(f => f.DataInicio).ToListAsync();
        }
    }
}