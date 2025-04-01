using System.Linq.Expressions;
using esii_2025_d1.Models;

namespace esii_2025_d1.Data;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    //public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    //    : base(options)
    //{
    //}
    
    public DbSet<Jom> Joms { get; set; } = null!;
    public DbSet<Log> Logs { get; set; } = null!;

   // public DbSet<User> Users { get; set; } = null!;
    //public DbSet<Customer> Customers { get; set; } = null!;
    //public DbSet<Project> Projects { get; set; } = null!;
    //public DbSet<Role> Roles { get; set; } = null!;
    //public DbSet<Task_m> Tasks { get; set; } = null!;
    //public DbSet<Permission> Permissions { get; set; } = null!;
    //public DbSet<ProjectUser> ProjectUsers { get; set; } = null!;


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
        //modelBuilder.Entity<Jom>().HasData(
        //    new Jom 
        //    { 
        //        Id = 1,
        //        Label = "Joms", 
        //        Date = DateTime.UtcNow, 
        //        IsDone = false, 
        //        TestNumber = 2.5f, 
        //        created_at = DateTime.UtcNow, 
        //        updated_at = DateTime.UtcNow,
        //        deleted_at = null
        //    },
        //    new Jom 
        //    { 
        //        Id = 2,
        //        Label = "Joms2", 
        //        Date = DateTime.UtcNow, 
        //        IsDone = true, 
        //        TestNumber = 7.5f, 
        //        created_at = DateTime.UtcNow, 
        //        updated_at = DateTime.UtcNow,
        //        deleted_at = null
        //    },
        //    new Jom 
        //    { 
        //        Id = 3,
        //        Label = "Joms3", 
        //        Date = DateTime.UtcNow, 
        //        IsDone = true, 
        //        TestNumber = 10, 
        //        created_at = DateTime.UtcNow, 
        //        updated_at = DateTime.UtcNow,
        //        deleted_at = null
        //    },
        //    new Jom 
        //    { 
        //        Id = 4,
        //        Label = "Joms4", 
        //        Date = DateTime.UtcNow, 
        //        IsDone = false, 
        //        TestNumber = 62, 
        //        created_at = DateTime.UtcNow, 
        //        updated_at = DateTime.UtcNow,
        //        deleted_at = null
        //    }
        //);
    }


}
