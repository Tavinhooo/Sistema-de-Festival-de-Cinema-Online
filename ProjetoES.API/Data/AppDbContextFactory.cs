using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ProjetoES.API.Data;

/// <summary>
/// Fábrica para criar instâncias do AppDbContext em tempo de design, utilizada para migrações e outras operações de design-time do Entity Framework Core.
/// Configura a string de conexão para o banco de dados PostgreSQL, permitindo que as migrações sejam aplicadas corretamente ao banco de dados durante o desenvolvimento e implantação da aplicação.
/// </summary>
public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        var connectionString = "Host=localhost;Port=5432;Database=cinema_festival;Username=postgres;Password=1234";
        optionsBuilder.UseNpgsql(connectionString);
        return new AppDbContext(optionsBuilder.Options);
    }
}
