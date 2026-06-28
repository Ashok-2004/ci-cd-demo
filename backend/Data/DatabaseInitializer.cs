using EmployeeHub.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

/*
 * DatabaseInitializer.cs adds demo data when the database is empty.
 *
 * Responsibility:
 * - Create a beginner-friendly account for demos.
 * - Add a few employees so the dashboard has data right after Docker Compose starts.
 *
 * Connection to other files:
 * - Program.cs calls this after EF Core ensures the database exists.
 * - AppDbContext performs the actual SQL Server reads and writes.
 */
namespace EmployeeHub.Api.Data;

public static class DatabaseInitializer
{
    public static async Task SeedAsync(AppDbContext dbContext)
    {
        if (!await dbContext.Users.AnyAsync())
        {
            var demoUser = new User
            {
                FullName = "Demo Admin",
                Email = "admin@employeehub.local"
            };

            demoUser.PasswordHash = new PasswordHasher<User>()
                .HashPassword(demoUser, "Admin@123");

            dbContext.Users.Add(demoUser);
        }

        if (!await dbContext.Employees.AnyAsync())
        {
            dbContext.Employees.AddRange(
                new Employee
                {
                    FullName = "Aarav Sharma",
                    Email = "aarav.sharma@employeehub.local",
                    Department = "Engineering",
                    JobTitle = "Backend Developer",
                    PhoneNumber = "555-0101",
                    HireDate = new DateOnly(2024, 1, 15),
                    CreatedAt = DateTime.UtcNow.AddDays(-2)
                },
                new Employee
                {
                    FullName = "Maya Patel",
                    Email = "maya.patel@employeehub.local",
                    Department = "People Operations",
                    JobTitle = "HR Specialist",
                    PhoneNumber = "555-0102",
                    HireDate = new DateOnly(2023, 9, 4),
                    CreatedAt = DateTime.UtcNow.AddDays(-1)
                },
                new Employee
                {
                    FullName = "Daniel Lee",
                    Email = "daniel.lee@employeehub.local",
                    Department = "Product",
                    JobTitle = "Product Analyst",
                    PhoneNumber = "555-0103",
                    HireDate = new DateOnly(2025, 3, 10),
                    CreatedAt = DateTime.UtcNow
                });
        }

        await dbContext.SaveChangesAsync();
    }
}
