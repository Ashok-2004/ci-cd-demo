/*
 * AuthDtos.cs contains the data shapes used by authentication endpoints.
 *
 * Responsibility:
 * - Define what the React frontend sends for register and login.
 * - Define what the API sends back after successful authentication.
 *
 * Connection to other files:
 * - AuthController receives these DTOs in HTTP requests.
 * - AuthService validates the data and returns AuthResponse.
 */
namespace EmployeeHub.Api.DTOs;

public record RegisterRequest(string FullName, string Email, string Password);

public record LoginRequest(string Email, string Password);

public record AuthResponse(string Token, string Email, string FullName);
