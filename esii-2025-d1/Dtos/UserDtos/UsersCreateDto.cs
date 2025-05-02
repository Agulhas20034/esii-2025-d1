namespace esii_2025_d1.Dtos.UsersDtos;

public class UsersCreateDto
{
    public int RoleId { get; set; }
    public string? Name { get; set; } = string.Empty;

    public string? Email { get; set; } = string.Empty;

    public required string Password { get; set; }

    public int DailyWorkHours { get; set; } = 0;

}