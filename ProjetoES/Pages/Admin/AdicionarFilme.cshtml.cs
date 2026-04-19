using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjetoES.Interfaces;
using ProjetoES.Factories;
using System.ComponentModel.DataAnnotations;

namespace ProjetoES.Pages.Admin
{
    // Mais tarde podemos adicionar aqui a tag [Authorize(Roles = "Administrador")]
    // para garantir que clientes normais não entram nesta página!
    public class AdicionarFilmeModel : PageModel
    {
        private readonly IFilmeService _filmeService;

        // Injeção de Dependência (DIP - SOLID)
        public AdicionarFilmeModel(IFilmeService filmeService)
        {
            _filmeService = filmeService;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new();

        public bool Sucesso { get; set; } = false;

        // O InputModel separa a validação visual da Base de Dados (SRP - SOLID)
        public class InputModel
        {
            [Required(ErrorMessage = "O título é obrigatório.")]
            public string Titulo { get; set; } = string.Empty;

            [Required(ErrorMessage = "A sinopse é obrigatória.")]
            public string Sinopse { get; set; } = string.Empty;

            [Required(ErrorMessage = "O género é obrigatório.")]
            public string Genero { get; set; } = string.Empty;

            [Required]
            [Range(1888, 2100, ErrorMessage = "Ano inválido.")]
            public int Ano { get; set; } = DateTime.Now.Year;

            [Required]
            [Range(1, 600, ErrorMessage = "Duração inválida.")]
            public int DuracaoMinutos { get; set; }

            [Required]
            [Range(0.01, 100.00, ErrorMessage = "Preço inválido.")]
            public decimal PrecoBilhete { get; set; }

            public string PosterUrl { get; set; } = string.Empty;
        }

        public void OnGet()
        {
            // Entra aqui quando a página carrega a primeira vez
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            // 1. Usar o Padrão Factory Method para criar a entidade de forma segura
            var novoFilme = FilmeFactory.CriarFilme(
                Input.Titulo,
                Input.Sinopse,
                Input.Genero,
                Input.Ano,
                Input.DuracaoMinutos,
                Input.PrecoBilhete,
                Input.PosterUrl
            );

            // 2. Usar a Interface para guardar na BD (Desacoplamento)
            await _filmeService.AdicionarFilmeAsync(novoFilme);

            // 3. Limpar o formulário e mostrar mensagem de sucesso
            Sucesso = true;
            ModelState.Clear();
            Input = new InputModel(); 

            return Page();
        }
    }
}