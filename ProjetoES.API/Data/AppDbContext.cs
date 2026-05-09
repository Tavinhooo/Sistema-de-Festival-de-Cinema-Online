using Microsoft.EntityFrameworkCore;
using ProjetoES.API.Models;

namespace ProjetoES.API.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Festival> Festivais { get; set; }
}