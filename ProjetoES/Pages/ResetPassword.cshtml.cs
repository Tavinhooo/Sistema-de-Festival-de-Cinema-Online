using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjetoES.Data;
using ProjetoES.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjetoES.Pages
{
    public class ResetPasswordModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly PasswordHasher<Utilizador> _passwordHasher;

        public ResetPasswordModel(ApplicationDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<Utilizador>();
        }

        // Estas propriedades apanham o Email e o Token que vêm no Link do Email
        [BindProperty(SupportsGet = true)]
        public string Email { get; set; } = string.Empty;

        [BindProperty(SupportsGet = true)]
        public string Token { get; set; } = string.Empty;

        [BindProperty]
        public InputModel Input { get; set; } = new();

        public class InputModel
        {
            [Required(ErrorMessage = "A password é obrigatória")]
            [StringLength(100, MinimumLength = 8, ErrorMessage = "A password deve ter no mínimo 8 caracteres.")]
            [RegularExpression(@"^(?=.*[!@#$%^&*(),.?""{}|<>]).*$", ErrorMessage = "A password deve conter pelo menos um caractere especial (!, @, #, etc).")]
            [DataType(DataType.Password)]
            public string Password { get; set; } = string.Empty;

            [Required(ErrorMessage = "A confirmação da password é obrigatória")]
            [Compare("Password", ErrorMessage = "As passwords não coincidem")]
            [DataType(DataType.Password)]
            public string ConfirmarPassword { get; set; } = string.Empty;
        }

        // Quando o utilizador clica no link do email, entra aqui
        public IActionResult OnGet()
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Token))
            {
                // Se alguém tentar entrar nesta página sem um link válido, mandamos para o Login
                return RedirectToPage("/Login"); 
            }
            return Page();
        }

        // Quando ele clica em "Guardar Password"
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            // 1. Verificar se o Token existe e se ainda está dentro do prazo (1 hora)
            var tokenValido = await _context.PasswordResetTokens
                .FirstOrDefaultAsync(t => t.Email == Email && t.Token == Token && t.DataExpiracao > DateTime.UtcNow);

            if (tokenValido == null)
            {
                ModelState.AddModelError(string.Empty, "O link de recuperação é inválido ou já expirou. Por favor, pede um novo link.");
                return Page();
            }

            // 2. Encontrar o Utilizador
            var user = await _context.Utilizadores.FirstOrDefaultAsync(u => u.Email == Email);
            
            if (user != null)
            {
                // 3. Atualizar a Password Encriptada
                user.PasswordHash = _passwordHasher.HashPassword(user, Input.Password);
                
                // 4. Mudar o Tipo de Utilizador (Opcional, só para garantir integridade, mas não é necessário)
                
                // 5. APAGAR o token usado por questões de segurança (Regra de Ouro!)
                _context.PasswordResetTokens.Remove(tokenValido);
                
                await _context.SaveChangesAsync();
            }

            // Manda o utilizador de volta para o Login para ele testar a nova password
            return RedirectToPage("/Login");
        }
    }
}