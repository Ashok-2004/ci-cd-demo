/*
 * JwtSettings.cs represents the JWT configuration from appsettings.json.
 *
 * Responsibility:
 * - Give strongly typed names to token settings such as issuer, audience, and secret.
 *
 * Connection to other files:
 * - Program.cs binds the Jwt section to this class.
 * - JwtTokenService reads these settings when creating tokens.
 */
namespace EmployeeHub.Api.Services;

public class JwtSettings
{
    public string Issuer { get; set; } = string.Empty;

    public string Audience { get; set; } = string.Empty;

    public string Secret { get; set; } = string.Empty;

    public int ExpirationMinutes { get; set; } = 60;
}
