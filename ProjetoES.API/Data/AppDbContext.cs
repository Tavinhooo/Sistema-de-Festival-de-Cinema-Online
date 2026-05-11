using Microsoft.EntityFrameworkCore;
using ProjetoES.API.Models;

namespace ProjetoES.API.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Festival> Festivais { get; set; }
    public DbSet<Filme> Filmes { get; set; }
    public DbSet<Sessao> Sessoes { get; set; }
    public DbSet<Carrinho> Carrinhos { get; set; }
    public DbSet<ItemCarrinho> ItensCarrinho { get; set; }
    public DbSet<Acesso> Acessos { get; set; }
    // TPH base - todos os utilizadores estão numa tabela "Visitantes"
    public DbSet<UtilizadorBase> UtilizadoresBase { get; set; }
    public DbSet<Visitante> Visitantes { get; set; }
    public DbSet<Utilizador> Utilizadores { get; set; }
    [System.Obsolete("Use Utilizadores")]
    public DbSet<Membro> Membros { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<ItemPedido> ItensPedido { get; set; }
    public DbSet<Avaliacao> Avaliacoes { get; set; }
    public DbSet<ListaPessoal> ListaPessoais { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // TPH (Table Per Hierarchy) - Base é UtilizadorBase, todos usam tabela "Visitantes"
        modelBuilder.Entity<UtilizadorBase>()
            .ToTable("Visitantes")
            .HasDiscriminator<string>("Discriminator")
            .HasValue<Visitante>("Visitante")
            .HasValue<Utilizador>("Utilizador")
            .HasValue<Membro>("Membro");
    }
}