using EmployeeHub.Api.DTOs;
using EmployeeHub.Api.Services;
using Microsoft.AspNetCore.Mvc;

/*
 * AuthController.cs receives authentication HTTP requests from the React frontend.
 *
 * Responsibility:
 * - Expose /api/auth/register and /api/auth/login.
 * - Forward register/login work to AuthService.
 * - Return JWT tokens that React stores and sends with protected employee requests.
 *
 * Connection to other files:
 * - AuthService contains the authentication business logic.
 * - RegisterRequest and LoginRequest define request body shapes.
 */
namespace EmployeeHub.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult<AuthResponse>> Register(RegisterRequest request)
    {
        try
        {
            var response = await authService.RegisterAsync(request);
            return Ok(response);
        }
        catch (InvalidOperationException exception)
        {
            return Conflict(new { message = exception.Message });
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login(LoginRequest request)
    {
        var response = await authService.LoginAsync(request);

        if (response is null)
        {
            return Unauthorized(new { message = "Invalid email or password." });
        }

        return Ok(response);
    }
}
