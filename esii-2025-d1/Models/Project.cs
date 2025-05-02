using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace esii_2025_d1.Models;

public class Project
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int UserId { get; set; }

    public int CustomerId { get; set; }
    public string? Name { get; set; } = string.Empty;

    [Range(0.0, 10.5, ErrorMessage = "the number must not be higher than 10.5")]
    public float? HourlyRate { get; set; } = 0;

    public int? DailyWorkHours { get; set; } = 0;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    
    public DateTime? DeletedAt { get; set; }
    
}