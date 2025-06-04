using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using esii_2025_d1.Models.Enums;

namespace esii_2025_d1.Models;

public class Log
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public int? entity_id { get; set; } 
    
    public string entity_name { get; set; } 
    
    public string user_id { get; set; } 
    
    [EnumDataType(typeof(LogAction))]
    public LogAction action { get; set; } = LogAction.Undefined;
    
    public DateTime created_at { get; set; } = DateTime.UtcNow; 
    
}