using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace esii_2025_d1.Models;

public class Media
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int ProjectId { get; set; }

    public int ReportId { get; set; }

    public string Name { get; set; }

    public string Type { get; set; }

    public string Path { get; set; }
    
    public DateTime Created_at { get; set; } = DateTime.Now;
    
    public DateTime Updated_at { get; set; } = DateTime.Now;
    
    public DateTime? Deleted_at { get; set; }
    
}