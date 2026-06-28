using EmployeeHub.Api.Data;
using EmployeeHub.Api.Models;
using Microsoft.EntityFrameworkCore;

/*
 * UserRepository.cs communicates with the Users table through Entity Framework Core.
 *
 * Responsibility:
 * - Look up users by email during login.
 * - Save new users during registration.
 *
 * Connection to other files:
 * - AuthService calls this repository.
 * - AppDbContext provides DbSet<User>.
 */
namespace EmployeeHub.Api.Repositories;

public class UserRepository(AppDbContext dbContext) : IUserRepository
{
    public Task<User?> GetByEmailAsync(string email)
    {
        var normalizedEmail = email.Trim().ToLower();

        return dbContext.Users.FirstOrDefaultAsync(user => user.Email.ToLower() == normalizedEmail);
    }

    public Task<bool> EmailExistsAsync(string email)
    {
        var normalizedEmail = email.Trim().ToLower();

        return dbContext.Users.AnyAsync(user => user.Email.ToLower() == normalizedEmail);
    }

    public async Task<User> AddAsync(User user)
    {
        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync();
        return user;
    }
}
