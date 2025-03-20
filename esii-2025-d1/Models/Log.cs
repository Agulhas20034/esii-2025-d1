using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace esii_2025_d1.Models;

public class Log
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public int? entity_id { get; set; } // This is the id of the entity that was modified
    
    public string entity_name { get; set; } // This is the name of the entity that was modified
    
    public int user_id { get; set; } // This is the id of the user that made the modification
    
    public string action { get; set; } // This is the action that was made on the entity
    
    public DateTime created_at { get; set; } = DateTime.UtcNow; // This is the date and time when the modification was made
    
}