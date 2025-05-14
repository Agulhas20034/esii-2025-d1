using System.ComponentModel.DataAnnotations;
using esii_2025_d1.Models.Enums;

namespace esii_2025_d1.Dtos.AssignmentDtos;

public class AssignmentCreateDto
{
    public string UserId { get; set; }
    public int? ProjectId { get; set; }

    [StringLength(500)]
    public string? Description { get; set; } = string.Empty;
    
    
    [Range(0, float.MaxValue)]
    public float? HourlyRate { get; set; } = 0;
    public DateTime? StartDate { get; set; }
    
    public DateTime? EndDate { get; set; }

    public AssignmentStatus Status { get; set; } 

}