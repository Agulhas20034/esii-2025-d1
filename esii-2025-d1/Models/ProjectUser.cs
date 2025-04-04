using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace esii_2025_d1.Models;

public class ProjectUser
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public int UserId{ get; set; }
    public int InviterId { get; set; }
    public string Status { get; set; } //TODO: IMPLEMENTAR ESTADOS   
    public DateTime CreatedAt { get; set; } = DateTime.Now;   
    public DateTime UpdatedAt { get; set; } = DateTime.Now;   
    public DateTime? DeletedAt { get; set; }
    
}