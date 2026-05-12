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
public DbSet<UtilizadorBase> Utilizadores { get; set; }
    public DbSet<Visitante> Visitantes { get; set; }
public DbSet<Utilizador> UtilizadoresAutenticados { get; set; }
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
            .ToTable("Utilizadores")
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

<<<<<<< HEAD
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
=======
    // Festival ↔ Filmes (many-to-many)
    modelBuilder.Entity<Festival>()
        .HasMany(f => f.Filmes)
        .WithMany(fm => fm.Festivais)
        .UsingEntity(j => j.ToTable("FestivalFilme"));

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

    // Configure Morada as an owned type stored in the Utilizadores table
    modelBuilder.Entity<Utilizador>(eb =>
    {
        eb.OwnsOne(u => u.MoradaFaturacao, mb =>
        {
            mb.Property(m => m.NomeDestinatario).HasColumnName("Morada_NomeDestinatario");
            mb.Property(m => m.MoradaFaturacao).HasColumnName("Morada_MoradaFaturacao");
            mb.Property(m => m.CodigoPostal).HasColumnName("Morada_CodigoPostal");
            mb.Property(m => m.Localidade).HasColumnName("Morada_Localidade");
            mb.Property(m => m.Pais).HasColumnName("Morada_Pais");
        });
    });
}
>>>>>>> f55a23f3e53de4c35a691b3b8e4364e0f87fd46b
}