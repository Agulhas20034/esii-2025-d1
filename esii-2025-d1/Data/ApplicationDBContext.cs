using System.Linq.Expressions;
using esii_2025_d1.Models;
using esii_2025_d1.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace esii_2025_d1.Data;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    
    public DbSet<Log> logs { get; set; } = null!;
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Media> Media { get; set; } = null!;
    public DbSet<Project> Projects { get; set; } = null!;
    public DbSet<ProjectUser> ProjectUsers { get; set; } = null!;
    public DbSet<ProjectReport> Reports { get; set; } = null!;
    public DbSet<Assignment> Assignments { get; set; } = null!;

    public DbSet<UserInfo> UserInfos { get; set; } = null!;



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);

        // Soft delete
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var deletedAtProperty = entityType.FindProperty("DeletedAt");
            if (deletedAtProperty != null && deletedAtProperty.ClrType == typeof(DateTime?))
            {
                var parameter = Expression.Parameter(entityType.ClrType, "e");
                var property = Expression.Property(parameter, "DeletedAt");
                var nullValue = Expression.Constant(null, typeof(DateTime?));
                var filter = Expression.Lambda(Expression.Equal(property, nullValue), parameter);

                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(filter);
            }
        }
    }
}
