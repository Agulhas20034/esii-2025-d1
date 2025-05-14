using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace esii_2025_d1.Dtos.UserDtos;

public class UserCreateDto
{
    public int UserId { get; set; }
    public int Name { get; set; }

}