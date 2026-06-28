using EmployeeHub.Api.Data;
using EmployeeHub.Api.DTOs;
using EmployeeHub.Api.Repositories;
using EmployeeHub.Api.Services;
using Microsoft.EntityFrameworkCore;

/*
 * EmployeeServiceTests.cs verifies employee business behavior.
 *
 * Responsibility:
 * - Test service logic without needing a real SQL Server database.
 * - Give the CI pipeline a meaningful backend test.
 *
 * Connection to other files:
 * - Uses AppDbContext with EF Core InMemory.
 * - Uses EmployeeRepository and EmployeeService the same way the API does.
 */
namespace EmployeeHub.Api.Tests;

public class EmployeeServiceTests
{
    [Fact]
    public async Task AddEmployeeAsync_ShouldIncreaseDashboardTotal()
    {
        var dbContext = CreateDbContext();
        var repository = new EmployeeRepository(dbContext);
        var service = new EmployeeService(repository);

        await service.AddEmployeeAsync(new CreateEmployeeRequest(
            "Priya Nair",
            "priya.nair@example.com",
            "Engineering",
            "Frontend Developer",
            "555-0199",
            new DateOnly(2025, 5, 1)));

        var dashboard = await service.GetDashboardSummaryAsync();

        Assert.Equal(1, dashboard.TotalEmployees);
        Assert.Equal("Priya Nair", dashboard.RecentEmployees[0].FullName);
    }

    private static AppDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }
}
