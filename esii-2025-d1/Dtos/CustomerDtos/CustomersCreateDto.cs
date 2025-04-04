namespace esii_2025_d1.Dtos.CustomersDtos;

public class CustomersCreateDto 
{
    // assume-se que o ID é automáticamente atribuído em todos os que têm ID
    public string Name { get; set; } 
    public string Email { get; set; }
    public string? PhoneNumber { get; set; }
}