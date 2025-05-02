namespace esii_2025_d1.Dtos.ProjectDtos;
using esii_2025_d1.Models;

public class ProjectUpdateDto
{
    public int CustomerId { get; set; }
    public string Name { get; set; }
    public float HourlyRate { get; set; }
    public int DailyWorkHours { get; set; }
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

}