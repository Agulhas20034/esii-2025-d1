namespace esii_2025_d1.Dtos.TasksDtos;

public class TasksCreateDto
{
    public int UserId { get; set; }

    public int ProjectId { get; set; }

    public string? Description { get; set; } = string.Empty;

    public float? HourlyRate { get; set; } = 0;

    public DateTime? StartDate { get; set; }
    
    public DateTime? EndDate { get; set; }
    
    public string Status { get; set; }
}