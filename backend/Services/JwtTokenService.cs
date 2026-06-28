using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EmployeeHub.Api.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

/*
 * JwtTokenService.cs creates JWT access tokens.
 *
 * Responsibility:
 * - Put user identity claims into a signed token.
 * - Use settings from appsettings.json so token behavior is easy to change.
 *
 * Connection to other files:
 * - AuthService calls this class after register or login succeeds.
 * - Program.cs configures the API to validate tokens created here.
 */
namespace EmployeeHub.Api.Services;

public class JwtTokenService(IOptions<JwtSettings> jwtOptions) : IJwtTokenService
{
    public string CreateToken(User user)
    {
        var settings = jwtOptions.Value;
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Secret));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Name, user.FullName)
        };

        var token = new JwtSecurityToken(
            issuer: settings.Issuer,
            audience: settings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(settings.ExpirationMinutes),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
