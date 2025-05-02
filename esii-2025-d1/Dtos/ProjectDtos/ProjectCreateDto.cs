namespace esii_2025_d1.Dtos.ProjectDtos;
using esii_2025_d1.Models;
using System.ComponentModel.DataAnnotations;

public class ProjectCreateDto
{
    [Required]
    public User UserId { get; set; }
    public int CustomerId { get; set; }
    public string Name { get; set; }
    public float HourlyRate { get; set; } = 0;
    public int DailyWorkHours { get; set; } = 0;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}