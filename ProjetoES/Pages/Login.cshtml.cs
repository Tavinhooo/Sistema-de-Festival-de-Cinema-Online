using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using ProjetoES.Data;
using ProjetoES.Models;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;

namespace ProjetoES.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly PasswordHasher<Utilizador> _passwordHasher;

        public LoginModel(ApplicationDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<Utilizador>();
        }

        [BindProperty(SupportsGet = true)]
        public string? ReturnUrl { get; set; }

        [BindProperty]
        public InputModel Input { get; set; } = new();

        public class InputModel
        {
            [Required, EmailAddress]
            public string Email { get; set; } = string.Empty;

            [Required, DataType(DataType.Password)]
            public string Password { get; set; } = string.Empty;

            public bool RememberMe { get; set; }
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            Utilizador? user;

            try
            {
                user = await _context.Utilizadores.FirstOrDefaultAsync(u => u.Email == Input.Email);
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

            if (user != null)
            {
                var resultado = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, Input.Password);

                if (resultado == PasswordVerificationResult.Success)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, $"{user.PrimeiroNome} {user.UltimoNome}".Trim()),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Role, user.Tipo.ToString()),
                        new Claim("UserId", user.Id.ToString())
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = Input.RememberMe,
                        ExpiresUtc = Input.RememberMe ? DateTimeOffset.UtcNow.AddDays(30) : null
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme, 
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        return LocalRedirect(ReturnUrl);
                    }

                    return RedirectToPage("/Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Email ou password incorretos.");
            return Page();
        }
    }
}