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
    public DbSet<Compra> Compras { get; set; }
    public DbSet<ItemPedido> Itens { get; set; } // Items for Carrinho, Compra, and Pedido
    public DbSet<Acesso> Acessos { get; set; }
    // TPH base - todos os utilizadores estão numa tabela "Visitantes"
    public DbSet<UtilizadorBase> UtilizadoresBase { get; set; }
    public DbSet<Visitante> Visitantes { get; set; }
    public DbSet<Utilizador> Utilizadores { get; set; }
    [System.Obsolete("Use Utilizadores")]
    public DbSet<Membro> Membros { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<Avaliacao> Avaliacoes { get; set; }
    public DbSet<ListaPessoal> ListaPessoais { get; set; }

protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    // TPH
    modelBuilder.Entity<UtilizadorBase>()
        .ToTable("Visitantes")
        .HasDiscriminator<string>("Discriminator")
        .HasValue<Visitante>("Visitante")
        .HasValue<Utilizador>("Utilizador")
        .HasValue<Membro>("Membro");

    // Pedido → Utilizador
    modelBuilder.Entity<Pedido>()
        .HasOne(p => p.Utilizador)
        .WithMany(u => u.HistoricoCompras)
        .HasForeignKey(p => p.UtilizadorId)
        .OnDelete(DeleteBehavior.Restrict);

    // Avaliacao → Utilizador (Cliente)
    modelBuilder.Entity<Avaliacao>()
        .HasOne(a => a.Cliente)
        .WithMany()
        .HasForeignKey(a => a.ClienteId)
        .OnDelete(DeleteBehavior.Restrict);

    // Avaliacao → Filme
    modelBuilder.Entity<Avaliacao>()
        .HasOne(a => a.Filme)
        .WithMany()
        .HasForeignKey(a => a.FilmeId)
        .OnDelete(DeleteBehavior.Restrict);

    // Acesso → Utilizador (Cliente)
    modelBuilder.Entity<Acesso>()
        .HasOne(a => a.Cliente)
        .WithMany()
        .HasForeignKey(a => a.ClienteId)
        .OnDelete(DeleteBehavior.Restrict);

    // RF13: um Cliente só pode votar uma vez por filme
    modelBuilder.Entity<Avaliacao>()
        .HasIndex(a => new { a.ClienteId, a.FilmeId })
        .IsUnique();
}
}