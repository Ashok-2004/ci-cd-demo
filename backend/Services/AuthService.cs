using EmployeeHub.Api.DTOs;
using EmployeeHub.Api.Models;
using EmployeeHub.Api.Repositories;
using Microsoft.AspNetCore.Identity;

/*
 * AuthService.cs contains the authentication business logic.
 *
 * Responsibility:
 * - Register new users.
 * - Verify login credentials.
 * - Ask JwtTokenService to create a token after successful authentication.
 *
 * Connection to other files:
 * - AuthController receives HTTP requests and calls this service.
 * - UserRepository communicates with SQL Server.
 * - JwtTokenService creates the JWT returned to React.
 */
namespace EmployeeHub.Api.Services;

public class AuthService(
    IUserRepository userRepository,
    PasswordHasher<User> passwordHasher,
    IJwtTokenService jwtTokenService) : IAuthService
{
    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        if (await userRepository.EmailExistsAsync(request.Email))
        {
            throw new InvalidOperationException("A user with this email already exists.");
        }

        var user = new User
        {
            FullName = request.FullName.Trim(),
            Email = request.Email.Trim().ToLower()
        };

        user.PasswordHash = passwordHasher.HashPassword(user, request.Password);

        var savedUser = await userRepository.AddAsync(user);
        return CreateAuthResponse(savedUser);
    }

    public async Task<AuthResponse?> LoginAsync(LoginRequest request)
    {
        var user = await userRepository.GetByEmailAsync(request.Email);

        if (user is null)
        {
            return null;
        }

        var passwordResult = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);

        if (passwordResult == PasswordVerificationResult.Failed)
        {
            return null;
        }

        return CreateAuthResponse(user);
    }

    private AuthResponse CreateAuthResponse(User user)
    {
        return new AuthResponse(
            jwtTokenService.CreateToken(user),
            user.Email,
            user.FullName);
    }
}
