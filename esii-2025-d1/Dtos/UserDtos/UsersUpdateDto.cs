namespace esii_2025_d1.Dtos.UsersDtos;

public class UsersUpdateDto
{
    public int RoleId { get; set; }
    public string? Name { get; set; }

    public string? Email { get; set; }

    public required string Password { get; set; }

    public int DailyWorkHours { get; set; }

}