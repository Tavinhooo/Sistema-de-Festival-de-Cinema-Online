using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjetoES.Interfaces;
using ProjetoES.Factories;
using ProjetoES.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjetoES.Pages.Admin
{
    public class AdicionarFilmeModel : PageModel
    {
        private readonly IFilmeService _filmeService;
        private readonly IFestivalService _festivalService;
        private readonly ITmdbService _tmdbService; // Novo serviço!

        // Injetamos os TRÊS serviços
        public AdicionarFilmeModel(IFilmeService filmeService, IFestivalService festivalService, ITmdbService tmdbService)
        {
            _filmeService = filmeService;
            _festivalService = festivalService;
            _tmdbService = tmdbService;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new();

        public List<Festival> FestivaisDisponiveis { get; set; } = new();
        public bool Sucesso { get; set; } = false;

        public class InputModel
        {
            [Required(ErrorMessage = "Tens de escolher a que festival este filme pertence.")]
            public int FestivalId { get; set; }

            [Required(ErrorMessage = "O título é obrigatório.")]
            public string Titulo { get; set; } = string.Empty;

            [Required(ErrorMessage = "A sinopse é obrigatória.")]
            public string Sinopse { get; set; } = string.Empty;

            [Required(ErrorMessage = "O género é obrigatório.")]
            public string Genero { get; set; } = string.Empty;

            [Required]
            public int Ano { get; set; } = DateTime.Now.Year;

            [Required]
            public int DuracaoMinutos { get; set; }

            [Required]
            public decimal PrecoBilhete { get; set; }

            public string PosterUrl { get; set; } = string.Empty;
        }

        public async Task OnGetAsync()
        {
            FestivaisDisponiveis = await _festivalService.ObterTodosFestivaisAsync();
        }

        // NOVO: Este método é chamado via JavaScript para ir à API!
        public async Task<JsonResult> OnGetPesquisaTmdbAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query)) return new JsonResult(new List<TmdbMovie>());

            var resultados = await _tmdbService.PesquisarFilmesAsync(query);
            return new JsonResult(resultados);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            FestivaisDisponiveis = await _festivalService.ObterTodosFestivaisAsync();

            if (!ModelState.IsValid) return Page();

            var novoFilme = FilmeFactory.CriarFilme(
                Input.Titulo, Input.Sinopse, Input.Genero, Input.Ano,
                Input.DuracaoMinutos, Input.PrecoBilhete, Input.PosterUrl
            );

            novoFilme.FestivalId = Input.FestivalId;

            await _filmeService.AdicionarFilmeAsync(novoFilme);

            return RedirectToPage("/Filmes", new { festivalId = novoFilme.FestivalId });
        }
        // Vai buscar a Duração e os Géneros quando clicamos num poster
        public async Task<JsonResult> OnGetDetalhesTmdbAsync(int id)
        {
            var detalhes = await _tmdbService.ObterDetalhesFilmeAsync(id);
            return new JsonResult(detalhes);
        }
    }
}