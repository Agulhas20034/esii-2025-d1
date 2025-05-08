using System.ComponentModel.DataAnnotations;

namespace esii_2025_d1.Dtos.UserInfoDtos;

public class UserInfoCreateDto
{
    [Required]
    public string Name { get; set; }
    
    public int DailyWorkHours { get; set; } = 0;
}