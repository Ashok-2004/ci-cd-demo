using EmployeeHub.Api.DTOs;

/*
 * IEmployeeService.cs defines the employee business operations.
 *
 * Responsibility:
 * - Describe what the API can do with employees without mentioning SQL Server.
 * - Give EmployeeController a simple contract to call.
 *
 * Connection to other files:
 * - EmployeeController depends on this interface.
 * - EmployeeService implements this interface and calls IEmployeeRepository.
 */
namespace EmployeeHub.Api.Services;

public interface IEmployeeService
{
    Task<IReadOnlyList<EmployeeDto>> GetEmployeesAsync(string? search);

    Task<EmployeeDto?> GetEmployeeByIdAsync(int id);

    Task<EmployeeDto> AddEmployeeAsync(CreateEmployeeRequest request);

    Task<EmployeeDto?> UpdateEmployeeAsync(int id, UpdateEmployeeRequest request);

    Task<bool> DeleteEmployeeAsync(int id);

    Task<DashboardSummaryDto> GetDashboardSummaryAsync();
}
