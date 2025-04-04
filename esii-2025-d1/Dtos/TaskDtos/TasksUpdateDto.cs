namespace esii_2025_d1.Dtos.TasksDtos;

public class TasksUpdateDto
{
    public string? Label { get; set; }
    public DateTime Date { get; set; }
    public bool IsDone { get; set; }
    public float? TestNumber { get; set; }
}