using Microsoft.EntityFrameworkCore;
using ProjetoES.API.Data;
using ProjetoES.API.Models;

namespace ProjetoES.API.Repositories;

public class AuthRepository
{
    private readonly AppDbContext _context;

    public AuthRepository(AppDbContext context)
    {
        _context = context;
    }

    public Membro? ObterPorEmail(string email)
    {
        return _context.Set<Membro>().FirstOrDefault(u => u.Email == email);
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

    public void CriarMembro(Membro membro)
    {
        _context.Add(membro);
        _context.SaveChanges();
    }

    public Membro ConverterVisitanteEmMembro(int visitanteId, Membro membro)
    {
        _context.Database.ExecuteSqlInterpolated($@"
            UPDATE ""Visitantes""
            SET ""Discriminator"" = 'Membro',
                ""PrimeiroNome"" = {membro.PrimeiroNome},
                ""UltimoNome"" = {membro.UltimoNome},
                ""Email"" = {membro.Email},
                ""PasswordHash"" = {membro.PasswordHash},
                ""IsLogged"" = TRUE,
                ""MetodoPagamento"" = {membro.MetodoPagamento}
            WHERE ""Id"" = {visitanteId} AND ""Discriminator"" = 'Visitante'");

        return _context.Set<Membro>().First(u => u.Id == visitanteId);
    }

    public void AtualizarMembro(Membro membro)
    {
        _context.Set<Membro>().Update(membro);
        _context.SaveChanges();
    }
}
