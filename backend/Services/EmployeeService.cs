using EmployeeHub.Api.DTOs;
using EmployeeHub.Api.Models;
using EmployeeHub.Api.Repositories;

/*
 * EmployeeService.cs contains business logic before communicating with the repository.
 *
 * Responsibility:
 * - Validate simple employee rules.
 * - Convert request DTOs into Employee models.
 * - Convert Employee models back into response DTOs.
 *
 * Connection to other files:
 * - EmployeeController receives HTTP requests and calls this service.
 * - EmployeeRepository handles the actual database work.
 */
namespace EmployeeHub.Api.Services;

public class EmployeeService(IEmployeeRepository employeeRepository) : IEmployeeService
{
    public async Task<IReadOnlyList<EmployeeDto>> GetEmployeesAsync(string? search)
    {
        var employees = await employeeRepository.GetAllAsync(search);

        return employees.Select(ToDto).ToList();
    }

    public async Task<EmployeeDto?> GetEmployeeByIdAsync(int id)
    {
        var employee = await employeeRepository.GetByIdAsync(id);

        return employee is null ? null : ToDto(employee);
    }

    public async Task<EmployeeDto> AddEmployeeAsync(CreateEmployeeRequest request)
    {
        await EnsureEmailIsAvailableAsync(request.Email);

        var employee = new Employee
        {
            FullName = request.FullName.Trim(),
            Email = request.Email.Trim(),
            Department = request.Department.Trim(),
            JobTitle = request.JobTitle.Trim(),
            PhoneNumber = string.IsNullOrWhiteSpace(request.PhoneNumber) ? null : request.PhoneNumber.Trim(),
            HireDate = request.HireDate,
            CreatedAt = DateTime.UtcNow
        };

        var savedEmployee = await employeeRepository.AddAsync(employee);
        return ToDto(savedEmployee);
    }

    public async Task<EmployeeDto?> UpdateEmployeeAsync(int id, UpdateEmployeeRequest request)
    {
        await EnsureEmailIsAvailableAsync(request.Email, id);

        var employee = new Employee
        {
            Id = id,
            FullName = request.FullName.Trim(),
            Email = request.Email.Trim(),
            Department = request.Department.Trim(),
            JobTitle = request.JobTitle.Trim(),
            PhoneNumber = string.IsNullOrWhiteSpace(request.PhoneNumber) ? null : request.PhoneNumber.Trim(),
            HireDate = request.HireDate
        };

        var updatedEmployee = await employeeRepository.UpdateAsync(employee);
        return updatedEmployee is null ? null : ToDto(updatedEmployee);
    }

    public Task<bool> DeleteEmployeeAsync(int id)
    {
        return employeeRepository.DeleteAsync(id);
    }

    public async Task<DashboardSummaryDto> GetDashboardSummaryAsync()
    {
        var totalEmployees = await employeeRepository.CountAsync();
        var recentEmployees = await employeeRepository.GetRecentAsync(5);

        return new DashboardSummaryDto(totalEmployees, recentEmployees.Select(ToDto).ToList());
    }

    private async Task EnsureEmailIsAvailableAsync(string email, int? excludingEmployeeId = null)
    {
        if (await employeeRepository.EmailExistsAsync(email, excludingEmployeeId))
        {
            throw new InvalidOperationException("Another employee already uses this email address.");
        }
    }

    private static EmployeeDto ToDto(Employee employee)
    {
        return new EmployeeDto(
            employee.Id,
            employee.FullName,
            employee.Email,
            employee.Department,
            employee.JobTitle,
            employee.PhoneNumber,
            employee.HireDate,
            employee.CreatedAt);
    }
}
