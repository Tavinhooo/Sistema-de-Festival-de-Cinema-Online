using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using ProjetoES.Data;
using System.Security.Claims;
using ProjetoES.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(options =>
{
    // Define que o Cookie é o método principal
    options.DefaultScheme = Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme;

    // Define que o Google é o método de desafio (quando clicas no botão)
    options.DefaultChallengeScheme = Microsoft.AspNetCore.Authentication.Google.GoogleDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.LoginPath = "/Login"; // Se não tiver logado, vai para aqui
})


.AddGoogle(options =>
{
    options.ClientId = builder.Configuration["Authentication:Google:ClientId"] ?? "A_TUA_CHAVE";
    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"] ?? "O_TEU_SEGREDO";
    options.CallbackPath = "/signin-google";

    options.Events = new Microsoft.AspNetCore.Authentication.OAuth.OAuthEvents
    {
        OnTicketReceived = async context =>
        {
            // 1. Apanhar os dados que o Google enviou
            var email = context.Principal?.FindFirstValue(ClaimTypes.Email);
            var nomeGoogle = context.Principal?.FindFirstValue(ClaimTypes.GivenName) ?? "Utilizador";
            var apelidoGoogle = context.Principal?.FindFirstValue(ClaimTypes.Surname) ?? "Google";

            // 2. Chamar a nossa Base de Dados
            var db = context.HttpContext.RequestServices.GetRequiredService<ApplicationDbContext>();

            // 3. Verificar se o email já existe no nosso PostgreSQL
            var utilizador = await db.Utilizadores.FirstOrDefaultAsync(u => u.Email == email);

            if (utilizador == null)
            {
                // Se não existir, CRIAMOS A CONTA automaticamente!
                utilizador = new Cliente
                {
                    PrimeiroNome = nomeGoogle,
                    UltimoNome = apelidoGoogle,
                    Email = email,
                    Tipo = TipoUtilizador.Membro,
                    PasswordHash = "LOGIN_VIA_GOOGLE" // Não tem password real
                };
                
                db.Utilizadores.Add(utilizador);
                await db.SaveChangesAsync(); // Guarda no PostgreSQL
            }

            // 4. Reescrever o "Crachá" para ficar igual ao do nosso Login manual
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, $"{utilizador.PrimeiroNome} {utilizador.UltimoNome}".Trim()),
                new Claim(ClaimTypes.Email, utilizador.Email),
                new Claim(ClaimTypes.Role, utilizador.Tipo.ToString()),
                new Claim("UserId", utilizador.Id.ToString())
            };

            var identity = new ClaimsIdentity(claims, Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme);
            context.Principal = new ClaimsPrincipal(identity);
        }
    };
});

builder.Services.AddTransient<ProjetoES.Services.EmailService>();
// Regista a injeção de dependência para a Interface dos Filmes
builder.Services.AddScoped<ProjetoES.Interfaces.IFilmeService, ProjetoES.Services.FilmeService>();
builder.Services.AddScoped<ProjetoES.Interfaces.IFestivalService, ProjetoES.Services.FestivalService>();
// Regista o serviço do TMDB permitindo-lhe usar o HttpClient para ir à internet
builder.Services.AddHttpClient<ProjetoES.Interfaces.ITmdbService, ProjetoES.Services.TmdbService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
