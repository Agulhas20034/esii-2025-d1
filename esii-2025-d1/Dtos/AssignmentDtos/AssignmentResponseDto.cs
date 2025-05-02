using esii_2025_d1.Models.Enums;

namespace esii_2025_d1.Dtos.AssignmentDtos;

public class AssignmentResponseDto
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int? ProjectId { get; set; }

    public string? Description { get; set; }

    public float? HourlyRate { get; set; } = 0;
    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public AssignmentStatus Status { get; set; } 
    
}