using Microsoft.EntityFrameworkCore;
using ProjetoES.API.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ProjetoES.API.Data;
using ProjetoES.API.Repositories;
using ProjetoES.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

// PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// DI registration
builder.Services.AddScoped<FestivalRepository>();
builder.Services.AddScoped<FestivalService>();
builder.Services.AddScoped<FilmeRepository>();
builder.Services.AddScoped<FilmeService>();
builder.Services.AddScoped<SessaoRepository>();
builder.Services.AddScoped<SessaoService>();
builder.Services.AddScoped<CarrinhoRepository>();
builder.Services.AddScoped<CarrinhoService>();
builder.Services.AddScoped<PedidoRepository>();
builder.Services.AddScoped<CheckoutService>();
builder.Services.AddHttpClient<ITmdbService, TmdbService>();
// Auth DI
builder.Services.AddScoped<AuthRepository>();
builder.Services.AddScoped<AuthService>();

// JWT configuration
var jwtSection = builder.Configuration.GetSection("JwtSettings");
var jwtSecret = jwtSection["Secret"] ?? string.Empty;

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSection["Issuer"],
            ValidAudience = jwtSection["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret))
        };
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.MapOpenApi();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();