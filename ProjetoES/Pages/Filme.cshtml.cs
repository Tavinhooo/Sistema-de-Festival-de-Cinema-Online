using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjetoES.Interfaces;
using ProjetoES.Models;

namespace ProjetoES.Pages
{
    public class FilmeModel : PageModel
    {
        private readonly IFilmeService _filmeService;

        public FilmeModel(IFilmeService filmeService) => _filmeService = filmeService;

        public Filme? Filme { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Filme = await _filmeService.ObterFilmePorIdAsync(id);
            if (Filme == null) return RedirectToPage("/Filmes");
            return Page();
        }
    }
}