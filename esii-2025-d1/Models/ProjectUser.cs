using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using esii_2025_d1.Models.Enums;

namespace esii_2025_d1.Models;

public class ProjectUser
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public string UserId { get; set; }  // foreign key to the user ASP.NET Identity
    
    public int ProjectId { get; set; }
    
    public string InviterId { get; set; } // foreign key to the user ASP.NET Identity
    
    public ProjectUserStatus Status { get; set; } = ProjectUserStatus.Pending;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;   
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;   
    public DateTime? DeletedAt { get; set; }
    
    
    
}