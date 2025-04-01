using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace esii_2025_d1.Models;

public class Login
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    public int RoleId { get; set; }
    public string? Username { get; set; } = string.Empty;
    public required string Password { get; set; }
    
}