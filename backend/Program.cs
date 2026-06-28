using System.Text;
using EmployeeHub.Api.Data;
using EmployeeHub.Api.Models;
using EmployeeHub.Api.Repositories;
using EmployeeHub.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

/*
 * Program.cs is the entry point of the ASP.NET Core application.
 *
 * Responsibility:
 * - Create the web application.
 * - Register services, repositories, EF Core, JWT authentication, Swagger, and CORS.
 * - Configure middleware that runs for every HTTP request.
 * - Start listening for requests from the React frontend.
 *
 * Connection to other files:
 * - Controllers receive HTTP requests.
 * - Services contain business logic.
 * - Repositories talk to Entity Framework Core and SQL Server.
 * - appsettings.json provides connection strings and JWT settings.
 */

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Swagger UI lets beginners test API endpoints in a browser at /swagger.
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "EmployeeHub Demo API",
        Version = "v1",
        Description = "A small ASP.NET Core API used to demonstrate CI/CD."
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter a valid JWT token. Example: Bearer eyJhbGciOi..."
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactFrontend", policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:5173",
                "http://127.0.0.1:5173",
                "http://localhost:5174",
                "http://127.0.0.1:5174")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>()
    ?? throw new InvalidOperationException("JWT settings are missing.");
var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = signingKey
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<PasswordHasher<User>>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("ReactFrontend");
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/health", () => Results.Ok(new
{
    status = "Healthy",
    application = "EmployeeHub Demo API"
}));

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    await InitializeDatabaseWithRetryAsync(dbContext, logger);
}

app.Run();

static async Task InitializeDatabaseWithRetryAsync(AppDbContext dbContext, ILogger logger)
{
    const int maxAttempts = 10;

    for (var attempt = 1; attempt <= maxAttempts; attempt++)
    {
        try
        {
            await dbContext.Database.EnsureCreatedAsync();
            await DatabaseInitializer.SeedAsync(dbContext);
            return;
        }
        catch (Exception exception) when (attempt < maxAttempts)
        {
            logger.LogWarning(exception, "Database startup attempt {Attempt} failed. Retrying shortly.", attempt);
            await Task.Delay(TimeSpan.FromSeconds(5));
        }
        catch (Exception exception)
        {
            logger.LogWarning(exception, "Database startup failed after all retry attempts.");
        }
    }
}
