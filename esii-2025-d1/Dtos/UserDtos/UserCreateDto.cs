<<<<<<< HEAD
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

=======
>>>>>>> origin/develop
namespace esii_2025_d1.Dtos.UserDtos;

public class UserCreateDto
{
<<<<<<< HEAD
    public int UserId { get; set; }
    public int Name { get; set; }

=======
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string? PhoneNumber { get; set; }
    
>>>>>>> origin/develop
}