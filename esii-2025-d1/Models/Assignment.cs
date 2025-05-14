using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using esii_2025_d1.Models.Enums;

namespace esii_2025_d1.Models;

public class Assignment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    
    public int Id { get; set; }

    public string UserId { get; set; }

    public int? ProjectId { get; set; } = null;

    public string? Description { get; set; } = string.Empty;

    public float? HourlyRate { get; set; } = 0;
    public DateTime? StartDate { get; set; }
    
    public DateTime? StartDate { get; set; } = DateTime.Now;

    public DateTime? EndDate { get; set; } = DateTime.Now;
    
    [EnumDataType(typeof(AssignmentStatus))]
    public AssignmentStatus Status { get; set; } = AssignmentStatus.Created;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? DeletedAt { get; set; }
    
}