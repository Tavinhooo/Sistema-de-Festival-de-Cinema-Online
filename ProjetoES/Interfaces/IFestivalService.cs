using ProjetoES.Models;

namespace ProjetoES.Interfaces
{
    public interface IFestivalService
    {
        Task AdicionarFestivalAsync(Festival festival);
        Task<List<Festival>> ObterTodosFestivaisAsync();
    }
}