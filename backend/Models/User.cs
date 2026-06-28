/*
 * User.cs describes the application user table.
 *
 * Responsibility:
 * - Store login information for simple JWT authentication.
 * - Keep the password as a hash instead of storing the original password.
 *
 * Connection to other files:
 * - AuthService creates and validates users.
 * - UserRepository reads and writes users through AppDbContext.
 * - JwtTokenService uses user information to create JWT tokens.
 */
namespace EmployeeHub.Api.Models;

public class User
{
    public int Id { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
