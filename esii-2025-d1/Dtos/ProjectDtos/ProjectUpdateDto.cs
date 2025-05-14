using System.ComponentModel.DataAnnotations;
using esii_2025_d1.Dtos.AssignmentDtos;
using esii_2025_d1.Dtos.MediaDtos;
using esii_2025_d1.Dtos.ReportDtos;
using esii_2025_d1.Models.Enums;

namespace esii_2025_d1.Dtos.ProjectDtos
{
    public class ProjectUpdateDto
    {
        public string? UserId { get; set; }
        public int? CustomerId { get; set; }
        
        [Required]
        public string? Name { get; set; }
        
        [StringLength(500)]
        public string? Description { get; set; } = string.Empty;
        
        [Range(0, float.MaxValue)]
        public float? HourlyRate { get; set; }
        [Range(0, float.MaxValue)]
        public int? DailyWorkHours { get; set; }
        
        public ProjectStatus Status { get; set; }
        
        public List<int>? AssignmentIds { get; set; } = new List<int>();
        public List<int>? MediaIds { get; set; } = new List<int>();
        public List<int>? ReportIds { get; set; } = new List<int>();
    }
}