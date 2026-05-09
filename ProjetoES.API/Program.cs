using Microsoft.EntityFrameworkCore;
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

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.MapOpenApi();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();