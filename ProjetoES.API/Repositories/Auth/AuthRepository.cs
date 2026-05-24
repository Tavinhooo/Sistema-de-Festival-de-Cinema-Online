using Microsoft.EntityFrameworkCore;
using ProjetoES.API.Data;
using ProjetoES.API.Models;

namespace ProjetoES.API.Repositories;
/// <summary>
/// Repositório de autenticação, responsável por gerenciar as operações relacionadas à autenticação de utilizadores.
/// </summary>
public class AuthRepository
{
    private readonly AppDbContext _context;

    public AuthRepository(AppDbContext context)
    {
        _context = context;
    }

    public Utilizador? ObterPorEmail(string email)
    {
        return _context.Set<Utilizador>().FirstOrDefault(u => u.Email == email);
    }

    public Visitante? ObterVisitantePorId(int visitanteId)
    {
        return _context.Set<Visitante>().FirstOrDefault(v => v.Id == visitanteId);
    }

    public Visitante CriarVisitante()
    {
        var visitante = new Visitante { IsLogged = false };
        _context.Set<Visitante>().Add(visitante);
        _context.SaveChanges();
        return visitante;
    }

    public void CriarUtilizador(Utilizador utilizador)
    {
        _context.Add(utilizador);
        _context.SaveChanges();
    }

    public Utilizador ConverterVisitanteEmUtilizador(int visitanteId, Utilizador utilizador)
    {
        _context.Database.ExecuteSqlInterpolated($@"
                UPDATE ""Utilizadores""
                SET ""Discriminator"" = 'Utilizador',
                ""PrimeiroNome"" = {utilizador.PrimeiroNome},
                ""UltimoNome"" = {utilizador.UltimoNome},
                ""Email"" = {utilizador.Email},
                ""PasswordHash"" = {utilizador.PasswordHash},
                ""IsLogged"" = TRUE,
                ""MetodoPagamento"" = {utilizador.MetodoPagamento},
                ""Tipo"" = {(int)utilizador.Tipo}
            WHERE ""Id"" = {visitanteId} AND ""Discriminator"" = 'Visitante'");

        // Retornar o utilizador já construído sem fazer query pós-UPDATE
        // para evitar inconsistências de tracking do EF
        utilizador.Id = visitanteId;
        return utilizador;
    }

    public void AtualizarUtilizador(Utilizador utilizador)
    {
        _context.Set<Utilizador>().Update(utilizador);
        _context.SaveChanges();
    }
}
