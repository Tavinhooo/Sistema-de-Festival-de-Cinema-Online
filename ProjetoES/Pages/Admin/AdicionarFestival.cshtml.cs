using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjetoES.Interfaces;
using ProjetoES.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjetoES.Pages.Admin
{
    public class AdicionarFestivalModel : PageModel
    {
        private readonly IFestivalService _festivalService;

        public AdicionarFestivalModel(IFestivalService festivalService)
        {
            _festivalService = festivalService;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new();

        public bool Sucesso { get; set; } = false;

        public class InputModel
        {
            [Required(ErrorMessage = "O nome é obrigatório.")]
            public string Nome { get; set; } = string.Empty;

            [Required(ErrorMessage = "A descrição é obrigatória.")]
            public string Descricao { get; set; } = string.Empty;

            [Required]
            [DataType(DataType.Date)]
            public DateTime DataInicio { get; set; } = DateTime.Today;

            [Required]
            [DataType(DataType.Date)]
            public DateTime DataFim { get; set; } = DateTime.Today.AddDays(7);

            public string PosterUrl { get; set; } = string.Empty;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var novoFestival = new Festival
            {
                Nome = Input.Nome,
                Descricao = Input.Descricao,
                DataInicio = Input.DataInicio.ToUniversalTime(), // PostgreSQL prefere UTC
                DataFim = Input.DataFim.ToUniversalTime(),
                PosterUrl = string.IsNullOrWhiteSpace(Input.PosterUrl) ? "/images/default-festival.jpg" : Input.PosterUrl
            };

            await _festivalService.AdicionarFestivalAsync(novoFestival);

            Sucesso = true;
            ModelState.Clear();
            Input = new InputModel(); 

            return Page();
        }
    }
}