using esii_2025_d1.Models.Enums;

namespace esii_2025_d1.Dtos.CustomersDtos;

public class ProjectSimpleDto
{
    public int Id { get; set; }
    
    public string? UserId { get; set; }
    
    public string Name { get; set; }
    
    public ProjectStatus Status { get; set; }
}