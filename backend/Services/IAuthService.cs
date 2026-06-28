using EmployeeHub.Api.DTOs;

/*
 * IAuthService.cs defines authentication business operations.
 *
 * Responsibility:
 * - Describe register and login behavior for controllers.
 * - Hide user lookup, password hashing, and JWT creation details.
 *
 * Connection to other files:
 * - AuthController depends on this interface.
 * - AuthService implements it using UserRepository and JwtTokenService.
 */
namespace EmployeeHub.Api.Services;

public interface IAuthService
{
    Task<AuthResponse> RegisterAsync(RegisterRequest request);

    Task<AuthResponse?> LoginAsync(LoginRequest request);
}
