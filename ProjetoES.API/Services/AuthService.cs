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

        var membro = new Membro
        {
            PrimeiroNome = dto.PrimeiroNome,
            UltimoNome = dto.UltimoNome,
            Email = dto.Email,
            PasswordHash = hash
        };

        _repo.CriarMembro(membro);

        return GenerateToken(membro);
    }

    public AuthResponseDTO Login(AuthLoginDTO dto)
    {
        var membro = _repo.ObterPorEmail(dto.Email);
        if (membro == null)
            throw new ArgumentException("Credenciais inválidas.");

        if (!BCrypt.Net.BCrypt.Verify(dto.Password, membro.PasswordHash))
            throw new ArgumentException("Credenciais inválidas.");

        return GenerateToken(membro);
    }

    private AuthResponseDTO GenerateToken(Membro usuario)
    {
        var secret = _config["JwtSettings:Secret"] ?? throw new InvalidOperationException("JWT secret not configured");
        var issuer = _config["JwtSettings:Issuer"] ?? "ProjetoES";
        var audience = _config["JwtSettings:Audience"] ?? "ProjetoESUsers";
        var expiresMinutes = int.TryParse(_config["JwtSettings:ExpiresMinutes"], out var m) ? m : 60;

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
            new Claim(ClaimTypes.Role, "Cliente")
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

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return new AuthResponseDTO { Token = tokenString, ExpiresAt = expires };
    }
}
