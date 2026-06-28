/*
 * Employee.cs describes the Employee database table.
 *
 * Responsibility:
 * - Store the fields that belong to one employee.
 * - Give Entity Framework Core a C# shape that maps to SQL Server.
 *
 * Connection to other files:
 * - AppDbContext exposes DbSet<Employee> so EF Core can query employees.
 * - EmployeeRepository reads and writes Employee records.
 * - EmployeeService converts Employee models into DTOs for controllers.
 */
namespace EmployeeHub.Api.Models;

public class Employee
{
    public int Id { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Department { get; set; } = string.Empty;

    public string JobTitle { get; set; } = string.Empty;

    public string? PhoneNumber { get; set; }

    public DateOnly HireDate { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
