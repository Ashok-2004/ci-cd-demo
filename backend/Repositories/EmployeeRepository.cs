using EmployeeHub.Api.Data;
using EmployeeHub.Api.Models;
using Microsoft.EntityFrameworkCore;

/*
 * EmployeeRepository.cs communicates with SQL Server through Entity Framework Core.
 *
 * Responsibility:
 * - Run employee queries.
 * - Add, update, and delete employee records.
 * - Keep database details out of controllers and services.
 *
 * Connection to other files:
 * - AppDbContext provides DbSet<Employee>.
 * - EmployeeService calls this repository after applying business rules.
 */
namespace EmployeeHub.Api.Repositories;

public class EmployeeRepository(AppDbContext dbContext) : IEmployeeRepository
{
    public async Task<IReadOnlyList<Employee>> GetAllAsync(string? search)
    {
        IQueryable<Employee> query = dbContext.Employees.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var normalizedSearch = search.Trim().ToLower();

            query = query.Where(employee =>
                employee.FullName.ToLower().Contains(normalizedSearch) ||
                employee.Email.ToLower().Contains(normalizedSearch) ||
                employee.Department.ToLower().Contains(normalizedSearch) ||
                employee.JobTitle.ToLower().Contains(normalizedSearch));
        }

        return await query
            .OrderBy(employee => employee.FullName)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Employee>> GetRecentAsync(int count)
    {
        return await dbContext.Employees
            .AsNoTracking()
            .OrderByDescending(employee => employee.CreatedAt)
            .Take(count)
            .ToListAsync();
    }

    public Task<int> CountAsync()
    {
        return dbContext.Employees.CountAsync();
    }

    public Task<Employee?> GetByIdAsync(int id)
    {
        return dbContext.Employees
            .AsNoTracking()
            .FirstOrDefaultAsync(employee => employee.Id == id);
    }

    public async Task<Employee> AddAsync(Employee employee)
    {
        dbContext.Employees.Add(employee);
        await dbContext.SaveChangesAsync();
        return employee;
    }

    public async Task<Employee?> UpdateAsync(Employee employee)
    {
        var existingEmployee = await dbContext.Employees.FindAsync(employee.Id);

        if (existingEmployee is null)
        {
            return null;
        }

        existingEmployee.FullName = employee.FullName;
        existingEmployee.Email = employee.Email;
        existingEmployee.Department = employee.Department;
        existingEmployee.JobTitle = employee.JobTitle;
        existingEmployee.PhoneNumber = employee.PhoneNumber;
        existingEmployee.HireDate = employee.HireDate;

        await dbContext.SaveChangesAsync();
        return existingEmployee;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var employee = await dbContext.Employees.FindAsync(id);

        if (employee is null)
        {
            return false;
        }

        dbContext.Employees.Remove(employee);
        await dbContext.SaveChangesAsync();
        return true;
    }

    public Task<bool> EmailExistsAsync(string email, int? excludingEmployeeId = null)
    {
        var normalizedEmail = email.Trim().ToLower();

        return dbContext.Employees.AnyAsync(employee =>
            employee.Email.ToLower() == normalizedEmail &&
            (!excludingEmployeeId.HasValue || employee.Id != excludingEmployeeId.Value));
    }
}
