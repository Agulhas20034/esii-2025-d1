namespace esii_2025_d1.Dtos.TasksDtos;

public class TasksResponseDto
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int ProjectId { get; set; }

    public string? Description { get; set; }

    public float? HourlyRate { get; set; } = 0;
    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string Status { get; set; } 

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

}