using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Npgsql;
using ProjetoES.Data;
using ProjetoES.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjetoES.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly PasswordHasher<Utilizador> _passwordHasher;

        public RegisterModel(ApplicationDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<Utilizador>();
        }

        [BindProperty]
        public InputModel Input { get; set; } = new();

        public class InputModel
        {
            [Required(ErrorMessage = "O nome é obrigatório")]
            // Aceita maiúsculas, minúsculas, letras com acentos (À-ÿ) e espaços
            [RegularExpression(@"^[a-zA-ZÀ-ÿ\s]+$", ErrorMessage = "O nome só pode conter letras.")]
            public string Nome { get; set; } = string.Empty;

            [Required(ErrorMessage = "O apelido é obrigatório")]
            [RegularExpression(@"^[a-zA-ZÀ-ÿ\s]+$", ErrorMessage = "O apelido só pode conter letras.")]
            public string Apelido { get; set; } = string.Empty;

            [Required(ErrorMessage = "O Email é obrigatório")]
            // O EmailAddress já obriga a ter '@' e um formato tipo "x@y.z"
            [EmailAddress(ErrorMessage = "Formato de email inválido.")]
            public string Email { get; set; } = string.Empty;

            [Required(ErrorMessage = "A password é obrigatória")]
            [StringLength(100, MinimumLength = 8, ErrorMessage = "A password deve ter no mínimo 8 caracteres.")]
            // Obriga a ter pelo menos 1 caractere especial
            [RegularExpression(@"^(?=.*[!@#$%^&*(),.?""{}|<>]).*$", ErrorMessage = "A password deve conter pelo menos um caractere especial (!, @, #, etc).")]
            [DataType(DataType.Password)]
            public string Password { get; set; } = string.Empty;

            [Required(ErrorMessage = "A confirmação da password é obrigatória")]
            [Compare("Password", ErrorMessage = "As passwords não coincidem")]
            [DataType(DataType.Password)]
            public string ConfirmarPassword { get; set; } = string.Empty;
        }

        public void OnGet() { } 

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            try
            {
                var existe = _context.Utilizadores.Any(u => u.Email == Input.Email);
                if (existe)
                {
                    ModelState.AddModelError("Input.Email", "Este email já está registado.");
                    return Page();
                }

                var novoUser = new Cliente
                {
                    PrimeiroNome = Input.Nome,
                    UltimoNome = Input.Apelido,
                    Email = Input.Email,
                    Tipo = TipoUtilizador.Membro 
                };

                novoUser.PasswordHash = _passwordHasher.HashPassword(novoUser, Input.Password);

                _context.Utilizadores.Add(novoUser);
                await _context.SaveChangesAsync();
            }
            catch (NpgsqlException)
            {
                ModelState.AddModelError(string.Empty, "Base de dados indisponível. Tenta novamente daqui a pouco.");
                return Page();
            }
            catch (InvalidOperationException ex) when (ex.InnerException is NpgsqlException)
            {
                ModelState.AddModelError(string.Empty, "Base de dados indisponível. Tenta novamente daqui a pouco.");
                return Page();
            }

            return RedirectToPage("Login");
        }
    }
}