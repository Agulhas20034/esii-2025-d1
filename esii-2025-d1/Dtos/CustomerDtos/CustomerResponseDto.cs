using esii_2025_d1.Dtos.ProjectDtos;

namespace esii_2025_d1.Dtos.CustomersDtos;

public class CustomerResponseDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    
    public virtual List<ProjectSimpleDto>? Projects { get; set; } = new List<ProjectSimpleDto>();
}