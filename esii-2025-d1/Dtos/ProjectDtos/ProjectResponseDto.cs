using esii_2025_d1.Dtos.AssignmentDtos;
using esii_2025_d1.Dtos.MediaDtos;
using esii_2025_d1.Dtos.ReportDtos;
using esii_2025_d1.Models.Enums;

namespace esii_2025_d1.Dtos.ProjectDtos;

public class ProjectResponseDto
{
        public int Id { get; set; }
        
        public string? UserId { get; set; }
        
        public int CustomerId { get; set; }
        
        public string Name { get; set; }
        
        public string? Description { get; set; }
        
        public ProjectStatus Status { get; set; } 
        
        public float? HourlyRate { get; set; }
        
        public int? DailyWorkHours { get; set; }
        
        public virtual ICollection<AssignmentResponseDto>? Assignments { get; set; } = new List<AssignmentResponseDto>();
        
        public virtual ICollection<MediaResponseDto>? Media { get; set; } = new List<MediaResponseDto>();
        
        public virtual ICollection<ReportResponseDto>? Reports { get; set; } = new List<ReportResponseDto>();
}

