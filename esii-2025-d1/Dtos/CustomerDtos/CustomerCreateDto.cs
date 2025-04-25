namespace esii_2025_d1.Dtos.CustomersDtos;

public class CustomerCreateDto 
{
    public string Name { get; set; } 
    public string Email { get; set; }
    public string? PhoneNumber { get; set; }
    
    public List<int>? ProjectIds { get; set; } = new List<int>();
}