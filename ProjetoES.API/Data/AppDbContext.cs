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
    public DbSet<FestivalFilme> FestivalFilmes { get; set; }
    public DbSet<Sessao> Sessoes { get; set; }
    public DbSet<Carrinho> Carrinhos { get; set; }
    public DbSet<Compra> Compras { get; set; }
    public DbSet<ItemPedido> Itens { get; set; }
    public DbSet<Acesso> Acessos { get; set; }
    public DbSet<UtilizadorBase> UtilizadoresBase { get; set; }
    public DbSet<Visitante> Visitantes { get; set; }
    public DbSet<Utilizador> Utilizadores { get; set; }
    [System.Obsolete("Use Utilizadores")]
    public DbSet<Membro> Membros { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<Avaliacao> Avaliacoes { get; set; }
    public DbSet<ListaPessoal> ListaPessoais { get; set; }
    public DbSet<Administrador> Administradores { get; set; }
    public DbSet<LogAlteracaoAcesso> LogsAlteracaoAcessos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UtilizadorBase>()
            .ToTable("Utilizadores")
            .HasDiscriminator<string>("Discriminator")
            .HasValue<Visitante>("Visitante")
            .HasValue<Utilizador>("Utilizador")
            .HasValue<Membro>("Membro");

        modelBuilder.Entity<Pedido>()
            .HasOne(p => p.Utilizador)
            .WithMany(u => u.HistoricoCompras)
            .HasForeignKey(p => p.UtilizadorId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Avaliacao>()
            .HasOne(a => a.Cliente)
            .WithMany()
            .HasForeignKey(a => a.ClienteId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Avaliacao>()
            .HasOne(a => a.Filme)
            .WithMany()
            .HasForeignKey(a => a.FilmeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Festival>()
            .HasMany(f => f.Filmes)
            .WithMany(fm => fm.Festivais)
            .UsingEntity<FestivalFilme>(
                j => j
                    .HasOne(ff => ff.Filme)
                    .WithMany()
                    .HasForeignKey(ff => ff.FilmeId),
                j => j
                    .HasOne(ff => ff.Festival)
                    .WithMany()
                    .HasForeignKey(ff => ff.FestivalId),
                j =>
                {
                    j.ToTable("FestivalFilme");
                    j.Property(ff => ff.FestivalId).HasColumnName("FestivaisId");
                    j.Property(ff => ff.FilmeId).HasColumnName("FilmesId");
                    j.HasKey(ff => new { ff.FestivalId, ff.FilmeId });
                    j.Property(ff => ff.PrecoBilhete).HasColumnType("numeric(10,2)");
                });

        modelBuilder.Entity<ListaPessoal>()
            .HasMany(l => l.Filmes)
            .WithMany(f => f.ListasPessoais)
            .UsingEntity<Dictionary<string, object>>(
                "ListaPessoalFilme",
                j => j
                    .HasOne<Filme>()
                    .WithMany()
                    .HasForeignKey("FilmeId")
                    .OnDelete(DeleteBehavior.Cascade),
                j => j
                    .HasOne<ListaPessoal>()
                    .WithMany()
                    .HasForeignKey("ListaPessoalId")
                    .OnDelete(DeleteBehavior.Cascade),
                j =>
                {
                    j.ToTable("ListaPessoalFilme");
                    j.HasKey("ListaPessoalId", "FilmeId");
                });

        modelBuilder.Entity<Acesso>()
            .HasOne(a => a.Cliente)
            .WithMany()
            .HasForeignKey(a => a.ClienteId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Avaliacao>()
            .HasIndex(a => new { a.ClienteId, a.FilmeId })
            .IsUnique();

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
}