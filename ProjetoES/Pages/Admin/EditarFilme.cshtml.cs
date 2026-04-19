using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjetoES.Interfaces;
using ProjetoES.Models;

namespace ProjetoES.Pages.Admin
{
    public class EditarFilmeModel : PageModel
    {
        private readonly IFilmeService _filmeService;

        public EditarFilmeModel(IFilmeService filmeService)
        {
            _filmeService = filmeService;
        }

        [BindProperty]
        public Filme FilmeParaEditar { get; set; } = new();

        public bool Sucesso { get; set; } = false;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var filme = await _filmeService.ObterFilmePorIdAsync(id);
            if (filme == null) return RedirectToPage("/Admin/ListarFilmes");

            FilmeParaEditar = filme;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            await _filmeService.AtualizarFilmeAsync(FilmeParaEditar);
            Sucesso = true;

            return Page();
        }
    }
}