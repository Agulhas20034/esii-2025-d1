using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace esii_2025_d1.Models;

public class Permission
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public string Label { get; set; }
    
    public DateTime Created_at { get; set; } = DateTime.Now;
    
    public DateTime Updated_at { get; set; } = DateTime.Now;
    
    public DateTime? Deleted_at { get; set; }
    
}