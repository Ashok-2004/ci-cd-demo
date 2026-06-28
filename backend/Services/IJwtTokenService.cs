using EmployeeHub.Api.Models;

/*
 * IJwtTokenService.cs defines token creation.
 *
 * Responsibility:
 * - Give AuthService a simple method for creating JWTs.
 * - Keep token implementation details in one class.
 *
 * Connection to other files:
 * - AuthService calls this interface after successful register or login.
 * - JwtTokenService implements it using settings from appsettings.json.
 */
namespace EmployeeHub.Api.Services;

public interface IJwtTokenService
{
    string CreateToken(User user);
}
