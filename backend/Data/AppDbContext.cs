using EmployeeHub.Api.Models;
using Microsoft.EntityFrameworkCore;

/*
 * AppDbContext.cs is the Entity Framework Core database gateway.
 *
 * Responsibility:
 * - Tell EF Core which tables exist.
 * - Configure table rules such as required fields and unique email addresses.
 * - Connect repositories to SQL Server.
 *
 * Connection to other files:
 * - Program.cs registers this DbContext with a SQL Server connection string.
 * - Repositories receive AppDbContext through dependency injection.
 * - Models such as Employee and User become SQL Server tables through this class.
 */
namespace EmployeeHub.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Employee> Employees => Set<Employee>();

    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(employee => employee.Id);
            entity.Property(employee => employee.FullName).HasMaxLength(120).IsRequired();
            entity.Property(employee => employee.Email).HasMaxLength(160).IsRequired();
            entity.Property(employee => employee.Department).HasMaxLength(80).IsRequired();
            entity.Property(employee => employee.JobTitle).HasMaxLength(80).IsRequired();
            entity.Property(employee => employee.PhoneNumber).HasMaxLength(30);
            entity.HasIndex(employee => employee.Email).IsUnique();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(user => user.Id);
            entity.Property(user => user.FullName).HasMaxLength(120).IsRequired();
            entity.Property(user => user.Email).HasMaxLength(160).IsRequired();
            entity.Property(user => user.PasswordHash).IsRequired();
            entity.HasIndex(user => user.Email).IsUnique();
        });
    }
}
