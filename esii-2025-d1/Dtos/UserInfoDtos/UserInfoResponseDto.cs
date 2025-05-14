namespace esii_2025_d1.Dtos.UserInfoDtos;

public class UserInfoResponseDto
{
    public string UserId { get; set; }
    public string Name { get; set; }
    public int DailyWorkHours { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}