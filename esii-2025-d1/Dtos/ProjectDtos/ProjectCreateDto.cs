namespace esii_2025_d1.Dtos.ProjectDtos;
using esii_2025_d1.Models;
using System.ComponentModel.DataAnnotations;
using esii_2025_d1.Models.Enums;

namespace esii_2025_d1.Dtos.ProjectDtos;

public class ProjectCreateDto
{ 
    [Required]
    public string? UserId { get; set; }
    
    [Required]
    public int CustomerId { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Name { get; set; }
    
    [StringLength(500)]
    public string? Description { get; set; } = string.Empty;
    
    [Range(0, float.MaxValue)]
    public float? HourlyRate { get; set; }
    
    [Range(0, 24)]
    public int? DailyWorkHours { get; set; }
    
    public ProjectStatus Status { get; set; } = ProjectStatus.Created;
    
    public List<int>? AssignmentIds { get; set; } = new List<int>();
    public List<int>? MediaIds { get; set; } = new List<int>();
    public List<int>? ReportIds { get; set; } = new List<int>();
}