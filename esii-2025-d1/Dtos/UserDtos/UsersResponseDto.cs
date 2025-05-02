namespace esii_2025_d1.Dtos.UsersDtos;

public class UsersResponseDto
{
    public int Id { get; set; }

    public int RoleId { get; set; }
    public string? Name { get; set; } = string.Empty;

    public string? Email { get; set; } = string.Empty;

    public required string Password { get; set; }

    public int DailyWorkHours { get; set; } = 0;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public DateTime? DeletedAt { get; set; }

}