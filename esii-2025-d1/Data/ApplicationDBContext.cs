using System.Linq.Expressions;
using esii_2025_d1.Models;

namespace esii_2025_d1.Data;

using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<Customer> Customers { get; set; } = null;

    public DbSet<Log> logs { get; set; } = null!;

    public DbSet<Media> Media { get; set; } = null;

    public DbSet<Permission> Permissions { get; set; } = null;

    public DbSet<Project> Projects { get; set; } = null;

    public DbSet<ProjectUser> ProjectUsers { get; set; } = null;

    public DbSet<Report> Reports { get; set; } = null;

    public DbSet<Role> Roles { get; set; } = null;

    public DbSet<Tasks> Tasks { get; set; } = null;
    
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
    }
}
