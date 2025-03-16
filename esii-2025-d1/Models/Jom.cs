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
    
    [Range(0.0,10.5,ErrorMessage = "the number must not be higher than 10.5")] 
    public float? TestNumber { get; set; }
    
    public DateTime created_at { get; set; } = DateTime.Now;
    
    public DateTime updated_at { get; set; } = DateTime.Now;
    
    public DateTime? deleted_at { get; set; }
    
}