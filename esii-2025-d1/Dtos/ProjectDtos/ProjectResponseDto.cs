namespace esii_2025_d1.Dtos.ProjectDtos;
using esii_2025_d1.Models;

public class ProjectResponseDto
{
    public int Id { get; set; } 
    public User UserId { get; set; }
    public int CustomerId { get; set; }
    public string Name { get; set; }
    public float HourlyRate { get; set; }
    public int DailyWorkHours { get; set; }
    public ICollection<Assignment>? Assignments { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

}