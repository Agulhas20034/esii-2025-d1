using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace esii_2025_d1.Models;

public class UserInfo
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
<<<<<<< HEAD:esii-2025-d1/Models/User.cs

    public int RoleId { get; set; }
    public string? Name { get; set; } = string.Empty;
=======
>>>>>>> origin/develop:esii-2025-d1/Models/UserInfo.cs
    
    [Required]
    public string Name { get; set; }
    
    public int DailyWorkHours { get; set; } = 0;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? DeletedAt { get; set; }
    
    public string UserId { get; set; }  // foreign key to the user ASP.NET Identity
    
}