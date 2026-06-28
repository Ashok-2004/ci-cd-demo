using EmployeeHub.Api.Models;

/*
 * IEmployeeRepository.cs defines what employee database operations are available.
 *
 * Responsibility:
 * - Give the service layer a simple contract for employee data access.
 * - Keep services independent from the exact database technology.
 *
 * Connection to other files:
 * - EmployeeService depends on this interface.
 * - EmployeeRepository implements this interface using Entity Framework Core.
 * - Program.cs registers the interface with dependency injection.
 */
namespace EmployeeHub.Api.Repositories;

public interface IEmployeeRepository
{
    Task<IReadOnlyList<Employee>> GetAllAsync(string? search);

    Task<IReadOnlyList<Employee>> GetRecentAsync(int count);

    Task<int> CountAsync();

    Task<Employee?> GetByIdAsync(int id);

    Task<Employee> AddAsync(Employee employee);

    Task<Employee?> UpdateAsync(Employee employee);

    Task<bool> DeleteAsync(int id);

    Task<bool> EmailExistsAsync(string email, int? excludingEmployeeId = null);
}
