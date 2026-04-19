using ProjetoES.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjetoES.Data;
using ProjetoES.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjetoES.Pages
{
    public class ForgotPasswordModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly EmailService _emailService;

        public ForgotPasswordModel(ApplicationDbContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new();

        // Controla se mostramos o formulário ou a mensagem de sucesso
        public bool EmailEnviado { get; set; } = false;

        public class InputModel
        {
            [Required(ErrorMessage = "O email é obrigatório.")]
            [EmailAddress(ErrorMessage = "Email inválido.")]
            public string Email { get; set; } = string.Empty;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var user = await _context.Utilizadores.FirstOrDefaultAsync(u => u.Email == Input.Email);

            if (user != null)
            {
                // 1. Gerar um Token único e difícil de adivinhar
                var token = Guid.NewGuid().ToString();

                // 2. Guardar na Tabela PasswordResetTokens 
                var resetToken = new PasswordResetToken
                {
                    Email = user.Email,
                    Token = token,
                    DataExpiracao = DateTime.UtcNow.AddHours(1) // Expira em 1 hora
                };

                _context.PasswordResetTokens.Add(resetToken);
                await _context.SaveChangesAsync();

                // 3. Criar o link com o Token encrostado
                var resetLink = Url.Page(
                    "/ResetPassword", // A página que vamos criar no próximo passo
                    pageHandler: null,
                    values: new { email = user.Email, token = token },
                    protocol: Request.Scheme);

                // 4. Enviar o Email VERDADEIRO!
                var assunto = "Redefinir Password - ProjetoES";
                var mensagem = $@"
    <div style='font-family: Arial, sans-serif; max-width: 600px; margin: auto; padding: 20px; border: 1px solid #e0e0e0; border-radius: 10px;'>
        <h2 style='color: #6b439b;'>Olá, {user.PrimeiroNome}!</h2>
        <p>Recebemos um pedido para repor a password da tua conta.</p>
        <p>Clica no botão abaixo para criar uma nova password. Este link expira em 1 hora.</p>
        <div style='text-align: center; margin: 30px 0;'>
            <a href='{resetLink}' style='background-color: #c47cb8; color: white; padding: 12px 25px; text-decoration: none; border-radius: 5px; font-weight: bold;'>Redefinir Password</a>
        </div>
        <p style='font-size: 12px; color: #888;'>Se não pediste para mudar a password, podes ignorar este email em segurança.</p>
    </div>";

                await _emailService.EnviarEmailAsync(user.Email, assunto, mensagem);
            }

            // Mostra o ecrã de sucesso (independentemente de o email existir ou não)
            EmailEnviado = true;
            return Page();
        }
    }
}