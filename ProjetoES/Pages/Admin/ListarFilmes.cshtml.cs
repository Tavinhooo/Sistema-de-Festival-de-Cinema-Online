using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjetoES.Interfaces;
using ProjetoES.Models;

namespace ProjetoES.Pages.Admin
{
    public class ListarFilmesModel : PageModel
    {
        private readonly IFilmeService _filmeService;

        // Injeção de Dependência da nossa Interface!
        public ListarFilmesModel(IFilmeService filmeService)
        {
            _filmeService = filmeService;
        }

        // Esta lista vai guardar os filmes que vêm da Base de Dados
        public List<Filme> Filmes { get; set; } = new();

        public async Task OnGetAsync()
        {
            // Pede os filmes ao serviço quando a página carrega
            Filmes = await _filmeService.ObterTodosFilmesAsync();
        }
        public async Task<IActionResult> OnPostEliminarAsync(int id)
        {
            await _filmeService.EliminarFilmeAsync(id);
            return RedirectToPage(); // Recarrega a página já sem o filme
        }
    }
}