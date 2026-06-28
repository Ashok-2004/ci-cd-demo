using EmployeeHub.Api.DTOs;
using EmployeeHub.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

/*
 * EmployeesController.cs receives employee HTTP requests from the React frontend.
 *
 * Responsibility:
 * - Expose CRUD endpoints for employees.
 * - Pass work to EmployeeService instead of talking to the database directly.
 * - Protect employee data with JWT authentication.
 *
 * Connection to other files:
 * - EmployeeService contains business logic.
 * - EmployeeRepository communicates with SQL Server through Entity Framework Core.
 * - React services call these endpoints through Axios.
 */
namespace EmployeeHub.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/employees")]
public class EmployeesController(IEmployeeService employeeService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<EmployeeDto>>> GetEmployees([FromQuery] string? search)
    {
        var employees = await employeeService.GetEmployeesAsync(search);
        return Ok(employees);
    }

    [HttpGet("dashboard")]
    public async Task<ActionResult<DashboardSummaryDto>> GetDashboardSummary()
    {
        var summary = await employeeService.GetDashboardSummaryAsync();
        return Ok(summary);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<EmployeeDto>> GetEmployeeById(int id)
    {
        var employee = await employeeService.GetEmployeeByIdAsync(id);

        if (employee is null)
        {
            return NotFound(new { message = "Employee was not found." });
        }

        return Ok(employee);
    }

    [HttpPost]
    public async Task<ActionResult<EmployeeDto>> AddEmployee(CreateEmployeeRequest request)
    {
        try
        {
            var employee = await employeeService.AddEmployeeAsync(request);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employee);
        }
        catch (InvalidOperationException exception)
        {
            return Conflict(new { message = exception.Message });
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<EmployeeDto>> UpdateEmployee(int id, UpdateEmployeeRequest request)
    {
        try
        {
            var employee = await employeeService.UpdateEmployeeAsync(id, request);

            if (employee is null)
            {
                return NotFound(new { message = "Employee was not found." });
            }

            return Ok(employee);
        }
        catch (InvalidOperationException exception)
        {
            return Conflict(new { message = exception.Message });
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        var deleted = await employeeService.DeleteEmployeeAsync(id);

        if (!deleted)
        {
            return NotFound(new { message = "Employee was not found." });
        }

        return NoContent();
    }
}
