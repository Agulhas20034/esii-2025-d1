using System.Linq.Expressions;
using esii_2025_d1.Models;
using esii_2025_d1.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace esii_2025_d1.Data;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    
    public DbSet<Jom> Joms { get; set; } = null!;
    public DbSet<Log> logs { get; set; } = null!;
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Media> Media { get; set; } = null!;
    public DbSet<Project> Projects { get; set; } = null!;
    public DbSet<ProjectUser> ProjectUsers { get; set; } = null!;
    public DbSet<Report> Reports { get; set; } = null!;
    public DbSet<Assignment> Assignments { get; set; } = null!;
    
    public DbSet<UserInfo> UserInfos { get; set; } = null!;
    
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {   
        
        base.OnModelCreating(modelBuilder);
        
        // Soft delete
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var deletedAtProperty = entityType.FindProperty("deleted_at");
            if (deletedAtProperty != null && deletedAtProperty.ClrType == typeof(DateTime?))
            {
                var parameter = Expression.Parameter(entityType.ClrType, "e");
                var property = Expression.Property(parameter, "deleted_at");
                var nullValue = Expression.Constant(null, typeof(DateTime?));
                var filter = Expression.Lambda(Expression.Equal(property, nullValue), parameter);

                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(filter);
            }
        }
        
        // Seed data
        modelBuilder.Entity<Jom>().HasData(
            new Jom 
            { 
                Id = 1,
                Label = "Joms", 
                Date = DateTime.UtcNow, 
                IsDone = false, 
                TestNumber = 2.5f, 
                created_at = DateTime.UtcNow, 
                updated_at = DateTime.UtcNow,
                deleted_at = null
            }
        );
        
        modelBuilder.Entity<Assignment>().HasData(
            new Assignment
            {
                Id = 1,
                UserId = 1,
                ProjectId = 1,
                Description = "Test assignment",
                HourlyRate = 20.0f,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(7),
                Status = AssignmentStatus.Created,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                DeletedAt = null
            },
            new Assignment
            {
                Id = 2,
                UserId = 1,
                ProjectId = 1,
                Description = "Test assignment2",
                HourlyRate = 20.0f,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(6),
                Status = AssignmentStatus.Created,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                DeletedAt = null
            },
            new Assignment
            {
                Id = 3,
                UserId = 2,
                ProjectId = 2,
                Description = "Test assignment3",
                HourlyRate = 20.0f,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(3),
                Status = AssignmentStatus.Created,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                DeletedAt = null
            }
        );
        
        modelBuilder.Entity<Customer>().HasData(
            new Customer
            {
                Id = 1,
                Name = "Test Customer",
                Email = "test@gmail.com",
                PhoneNumber = "123456789",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                DeletedAt = null,
            },
            new Customer
            {
                Id = 2,
                Name = "Test Customer2",
                Email = "test2@gmail.com",
                PhoneNumber = "923456789",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                DeletedAt = null,
            }
        );
        
        modelBuilder.Entity<Project>().HasData(
            new Project
            {
                Id = 1,
                UserId = 1,
                CustomerId = 1,
                Name = "Test Project",
                Description = "Test project description",
                HourlyRate = 14.0f,
                DailyWorkHours = 8,
                Status = ProjectStatus.Created,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                DeletedAt = null,
            },
            new Project
            {
                Id = 2,
                UserId = 1,
                CustomerId = 1,
                Name = "Test Project2",
                Description = "Test project description2",
                HourlyRate = 16.0f,
                DailyWorkHours = 4,
                Status = ProjectStatus.Created,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                DeletedAt = null,
            },
            new Project
            {
                Id = 3,
                UserId = 1,
                CustomerId = 1,
                Name = "Test Project3",
                Description = "Test project description3",
                HourlyRate = 16.0f,
                DailyWorkHours = 4,
                Status = ProjectStatus.Created,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                DeletedAt = null,
            }
        );
        modelBuilder.Entity<Report>().HasData(
            new Report
            {
                Id = 1,
                ProjectId = 1,
                UserId = 1,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                DeletedAt = null
            },
            new Report
            {
                Id = 2,
                ProjectId = 2,
                UserId = 2,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                DeletedAt = null
            }
        );
        modelBuilder.Entity<Media>().HasData(
            new Media
            {
                Id = 1,
                ReportId = null,
                ProjectId = 1,
                Name = "test",
                Type = "image",
                Path = "test.jpg",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                DeletedAt = null
            },
            new Media
            {
                Id = 2,
                ReportId = 2,
                ProjectId = 2,
                Name = "test2",
                Type = "Report",
                Path = "test2.jpg",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                DeletedAt = null
                    
            }
        );
    }


}
