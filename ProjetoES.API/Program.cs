using Microsoft.EntityFrameworkCore;
using ProjetoES.API.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ProjetoES.API.Data;
using ProjetoES.API.Repositories;
using ProjetoES.API.Services;
using ProjetoES.API.Models;
using ProjetoES.API.Services.Recomendacoes;
using ProjetoES.API.Services.Premios;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
        policy.WithOrigins("http://localhost:5051", "https://localhost:7083")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

// PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// ==========================================
// REGISTRATIONS
// ==========================================
builder.Services.AddScoped<FilmeService>();
builder.Services.AddScoped<IAdministradorRepository, AdministradorRepository>();
builder.Services.AddScoped<IAdministradorService, AdministradorService>();
builder.Services.AddScoped<IListaPessoalRepository, ListaPessoalRepository>();
builder.Services.AddScoped<IListaPessoalService, ListaPessoalService>();

builder.Services.AddScoped<CheckoutService>();

builder.Services.AddScoped<FestivalRepository>();
builder.Services.AddScoped<FestivalService>();
builder.Services.AddScoped<FilmeRepository>();
builder.Services.AddScoped<SessaoRepository>();
builder.Services.AddScoped<SessaoService>();
builder.Services.AddScoped<AcessoRepository>();
builder.Services.AddScoped<AcessoService>();
builder.Services.AddScoped<AvaliacaoRepository>();
builder.Services.AddScoped<AvaliacaoService>();
builder.Services.AddScoped<CarrinhoRepository>();
builder.Services.AddScoped<CarrinhoService>();
builder.Services.AddScoped<PedidoRepository>();
builder.Services.AddScoped<CheckoutFacade>();
builder.Services.AddScoped<StripeCheckoutService>();

builder.Services.AddHttpClient<ITmdbApiClient, TmdbApiClient>();
builder.Services.AddScoped<ITmdbService, TmdbServiceAdapter>();

builder.Services.AddScoped<AuthRepository>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<MembroRepository>();
builder.Services.AddScoped<MembroService>();
builder.Services.AddScoped<ClienteRepository>();
builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<IRecomendacaoStrategy, PorGeneroStrategy>();
builder.Services.AddScoped<IRecomendacaoStrategy, PorPopularidadeStrategy>();
builder.Services.AddScoped<IRecomendacaoStrategy, PorFestivalStrategy>();
builder.Services.AddScoped<RecomendacoesService>();
builder.Services.AddScoped<IVotoObserver, LogVotoObserver>();
builder.Services.AddScoped<IVotoObserver, LiderPremioObserver>();
builder.Services.AddScoped<PremioService>();

// ==========================================
// JWT CONFIGURATION
// ==========================================
var jwtSection = builder.Configuration.GetSection("JwtSettings");
var jwtSecret = jwtSection["Secret"];

if (string.IsNullOrEmpty(jwtSecret))
{
    throw new InvalidOperationException("JWT Secret is missing from appsettings.json. Cannot initialize symmetric key.");
}

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.MapInboundClaims = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSection["Issuer"],
        ValidAudience = jwtSection["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)),
        RoleClaimType = "role",
        NameClaimType = "sub"
    };
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors("Frontend");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();