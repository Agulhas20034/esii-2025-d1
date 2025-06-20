using System.Text.Json;
using esii_2025_d1.Models;
using esii_2025_d1.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace esii_2025_d1.Data;

public static class ApplicationDbContextSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await context.Database.MigrateAsync();

        var userId = "f377d4bc-d61e-4d0a-8438-ed89e5cb3476";

        // Seed Customers
        if (!context.Customers.Any())
        {
            context.Customers.AddRange(
                new Customer
                {
                    Id = 1,
                    Name = "Test Customer",
                    Email = "test@gmail.com",
                    PhoneNumber = "123456789",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    DeletedAt = null
                },
                new Customer
                {
                    Id = 2,
                    Name = "Test Customer2",
                    Email = "test2@gmail.com",
                    PhoneNumber = "923456789",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    DeletedAt = null
                }
            );
            await context.SaveChangesAsync();
        }

        // Seed Projects
        if (!context.Projects.Any())
        {
            context.Projects.AddRange(
                new Project
                {
                    Id = 1,
                    CustomerId = 1,
                    UserId = userId,
                    Name = "Test Project",
                    Description = "Test project description",
                    HourlyRate = 14.0f,
                    DailyWorkHours = 8,
                    Status = ProjectStatus.Created,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    DeletedAt = null
                },
                new Project
                {
                    Id = 2,
                    CustomerId = 1,
                    UserId = userId,
                    Name = "Test Project2",
                    Description = "Test project description2",
                    HourlyRate = 16.0f,
                    DailyWorkHours = 4,
                    Status = ProjectStatus.Created,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    DeletedAt = null
                },
                new Project
                {
                    Id = 3,
                    CustomerId = 1,
                    UserId = userId,
                    Name = "Test Project3",
                    Description = "Test project description3",
                    HourlyRate = 16.0f,
                    DailyWorkHours = 4,
                    Status = ProjectStatus.Created,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    DeletedAt = null
                }
            );
            await context.SaveChangesAsync();
        }

        // Seed Assignments
        if (!context.Assignments.Any())
        {
            context.Assignments.AddRange(
                new Assignment
                {
                    Id = 1,
                    UserId = userId,
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
                    UserId = userId,
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
                    UserId = userId,
                    ProjectId = 2,
                    Description = "Test assignment3",
                    HourlyRate = 8.0f,
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddDays(6),
                    Status = AssignmentStatus.Completed,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    DeletedAt = null
                }
            );
            await context.SaveChangesAsync();
        }
        
        if (!context.Reports.Any())
        {
        var reportData1 = new ReportData
        {
            TotalHours = 40,
            TotalAmount = 560,
            DailyReports = new List<DailyReport>
        {
            new DailyReport
            {
                Date = DateTime.UtcNow.AddDays(-2),
                DailyHours = 8,
                DailyAmount = 112,
                ExceededDailyHours = false,
                Tasks = new List<ReportTask>
                {
                    new ReportTask
                    {
                        AssignmentId = 1,
                        ProjectId = 1,
                        ProjectName = "Test Project",
                        Description = "Task 1",
                        Hours = 8,
                        HourlyRate = 14,
                        Amount = 112
                    }
                }
            },
            new DailyReport
            {
                Date = DateTime.UtcNow.AddDays(-1),
                DailyHours = 8,
                DailyAmount = 112,
                ExceededDailyHours = false,
                Tasks = new List<ReportTask>
                {
                    new ReportTask
                    {
                        AssignmentId = 1,
                        ProjectId = 1,
                        ProjectName = "Test Project",
                        Description = "Task 2",
                        Hours = 8,
                        HourlyRate = 14,
                        Amount = 112
                    }
                }
            }
        }
    };

        var reportDataJson1 = JsonSerializer.Serialize(reportData1);

        context.Reports.Add(new ProjectReport
        {
            Id = 1,
            UserId = userId,
            ProjectId = 1,
            Title = "Monthly ProjectReport June",
            Description = "ProjectReport of tasks done in June",
            StartDate = DateTime.UtcNow.AddDays(-30),
            EndDate = DateTime.UtcNow,
            TotalHours = reportData1.TotalHours,
            TotalAmount = reportData1.TotalAmount,
            ReportDataJson = reportDataJson1,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            DeletedAt = null
        });

        await context.SaveChangesAsync();
    }

        // Seed Jom
        /*if (!context.Joms.Any())
        {
            context.Joms.Add(
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
            await context.SaveChangesAsync();
        }*/

        
    }
}
