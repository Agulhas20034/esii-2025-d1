using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace esii_2025_d1.Models;

public class Jom
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public string? Label { get; set; } = string.Empty;
    
    public DateTime Date { get; set; } = DateTime.Now;
    
    public bool IsDone { get; set; } = false;
    
    public float TestNumber { get; set; } = 0.0f;
    
    public DateTime created_at { get; set; } = DateTime.Now;
    
    public DateTime updated_at { get; set; } = DateTime.Now;
    
    public DateTime? deleted_at { get; set; }
    
}