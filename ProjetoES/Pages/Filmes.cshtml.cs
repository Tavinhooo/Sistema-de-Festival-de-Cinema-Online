using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjetoES.Interfaces;
using ProjetoES.Models;

namespace ProjetoES.Pages
{
    public class FilmesModel : PageModel
    {
        private readonly IFilmeService _filmeService;

        public FilmesModel(IFilmeService filmeService) => _filmeService = filmeService;

        public List<Filme> ListaFilmes { get; set; } = new();

        public async Task OnGetAsync()
        {
            ListaFilmes = await _filmeService.ObterTodosFilmesAsync();
        }
    }
}