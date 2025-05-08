using esii_2025_d1.Dtos.UserInfoDtos;

namespace esii_2025_d1.Dtos.UserDtos;

public class UserFullResponseDto
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public UserInfoResponseDto? UserInfo { get; set; }
}