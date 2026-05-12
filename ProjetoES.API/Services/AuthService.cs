using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ProjetoES.API.DTOS;
using ProjetoES.API.Models;
using ProjetoES.API.Repositories;

namespace ProjetoES.API.Services;

public class AuthService
{
    private readonly AuthRepository _repo;
    private readonly IConfiguration _config;

    public AuthService(AuthRepository repo, IConfiguration config)
    {
        _repo = repo;
        _config = config;
    }

    public AuthResponseDTO Register(AuthRegisterDTO dto)
    {
        var existing = _repo.ObterPorEmail(dto.Email);
        if (existing != null)
            throw new ArgumentException("Email já registado.");

        var hash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        var utilizador = new Utilizador
        {
            PrimeiroNome = dto.PrimeiroNome,
            UltimoNome = dto.UltimoNome,
            Email = dto.Email,
            PasswordHash = hash,
            IsLogged = true,
            Tipo = TipoUtilizador.Membro  // Registo cria sempre um Membro
        };

        if (dto.VisitanteId.HasValue)
        {
            var visitante = _repo.ObterVisitantePorId(dto.VisitanteId.Value);
            if (visitante == null)
                throw new ArgumentException("Visitante inválido.");

            if (!string.IsNullOrEmpty(visitante.Email))
                throw new ArgumentException("Este visitante já foi convertido em utilizador.");

            utilizador = _repo.ConverterVisitanteEmUtilizador(dto.VisitanteId.Value, utilizador);
        }
        else
        {
            _repo.CriarUtilizador(utilizador);
        }

        return GenerateToken(utilizador);
    }

    public AuthResponseDTO Login(AuthLoginDTO dto)
    {
        var utilizador = _repo.ObterPorEmail(dto.Email);
        if (utilizador == null)
            throw new ArgumentException("Credenciais inválidas.");

        if (!BCrypt.Net.BCrypt.Verify(dto.Password, utilizador.PasswordHash))
            throw new ArgumentException("Credenciais inválidas.");

        utilizador.IsLogged = true;
        _repo.AtualizarUtilizador(utilizador);

        return GenerateToken(utilizador);
    }

    // Chamado pelo MembroService
    public AuthResponseDTO GenerateTokenPublico(Utilizador utilizador)
        => GenerateToken(utilizador);

    private AuthResponseDTO GenerateToken(Utilizador usuario)
    {
        var secret = _config["JwtSettings:Secret"]
            ?? throw new InvalidOperationException("JWT secret not configured");
        var issuer = _config["JwtSettings:Issuer"] ?? "ProjetoES";
        var audience = _config["JwtSettings:Audience"] ?? "ProjetoESUsers";
        var expiresMinutes = int.TryParse(_config["JwtSettings:ExpiresMinutes"], out var m) ? m : 60;

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
            // FIX: usa o TipoUtilizador real — "Membro", "Cliente" ou "Administrador"
            new Claim(ClaimTypes.Role, usuario.Tipo.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.UtcNow.AddMinutes(expiresMinutes);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: expires,
            signingCredentials: creds
        );

        return new AuthResponseDTO
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            ExpiresAt = expires
        };
    }
}