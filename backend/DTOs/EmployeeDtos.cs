/*
 * EmployeeDtos.cs contains the data shapes for employee endpoints.
 *
 * Responsibility:
 * - Keep API input and output clear for the React frontend.
 * - Avoid exposing the database model directly over HTTP.
 *
 * Connection to other files:
 * - EmployeeController receives CreateEmployeeRequest and UpdateEmployeeRequest.
 * - EmployeeService returns EmployeeDto and DashboardSummaryDto.
 * - React TypeScript types should match these response shapes.
 */
namespace EmployeeHub.Api.DTOs;

public record EmployeeDto(
    int Id,
    string FullName,
    string Email,
    string Department,
    string JobTitle,
    string? PhoneNumber,
    DateOnly HireDate,
    DateTime CreatedAt);

public record CreateEmployeeRequest(
    string FullName,
    string Email,
    string Department,
    string JobTitle,
    string? PhoneNumber,
    DateOnly HireDate);

public record UpdateEmployeeRequest(
    string FullName,
    string Email,
    string Department,
    string JobTitle,
    string? PhoneNumber,
    DateOnly HireDate);

public record DashboardSummaryDto(int TotalEmployees, IReadOnlyList<EmployeeDto> RecentEmployees);
