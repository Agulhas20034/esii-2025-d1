namespace esii_2025_d1.Dtos.TasksDtos;

public class TasksUpdateDto
{
    public int UserId { get; set; }

    public int ProjectId { get; set; }

    public string? Description { get; set; }

    public float? HourlyRate { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string Status { get; set; }
}