namespace esii_2025_d1.Dtos.JomDtos;

public class JomResponseDto
{
    public int Id { get; set; }
    public string? Label { get; set; }
    public DateTime Date { get; set; }
    public bool IsDone { get; set; }
    public float? TestNumber { get; set; }
    public DateTime created_at { get; set; }
    public DateTime updated_at { get; set; } 
    
}