using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace esii_2025_d1.Models;

public class Task_m
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int UserId { get; set; }

    public int ProjectId { get; set; }

    public string? Description { get; set; } = string.Empty;

    public float? HourlyRate { get; set; } = 0;
    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }
    
    public string Satus { get; set; } //TODO: IMPLEMENT ENUM
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    
    public DateTime? DeletedAt { get; set; }
    
}