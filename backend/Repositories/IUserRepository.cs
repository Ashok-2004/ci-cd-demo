using EmployeeHub.Api.Models;

/*
 * IUserRepository.cs defines the database operations needed for users.
 *
 * Responsibility:
 * - Give AuthService a clean way to find and create users.
 * - Keep authentication logic separate from EF Core query details.
 *
 * Connection to other files:
 * - AuthService depends on this interface.
 * - UserRepository implements it using AppDbContext.
 */
namespace EmployeeHub.Api.Repositories;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);

    Task<bool> EmailExistsAsync(string email);

    Task<User> AddAsync(User user);
}
