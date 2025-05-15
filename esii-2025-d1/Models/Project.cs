using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using esii_2025_d1.Interfaces.ObserverPattern;
using esii_2025_d1.Models.Enums;

namespace esii_2025_d1.Models;

public class Project
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string UserId { get; set; }

    public int CustomerId { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public float? HourlyRate { get; set; } = 0;

    public int? DailyWorkHours { get; set; } = 0;
    
    [EnumDataType(typeof(ProjectStatus))]
    public ProjectStatus Status { get; set; } = ProjectStatus.Created;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? DeletedAt { get; set; }
    
    public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

    public virtual ICollection<ProjectUser> ProjectUsers { get; set; } = new List<ProjectUser>();

    public virtual ICollection<Media> Media { get; set; } = new List<Media>();
    
    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();
    
    
    private static readonly List<IProjectObserver> _observers = new();

    public static void Subscribe(IProjectObserver observer)
    {
        _observers.Add(observer);
    }

    public static void Unsubscribe(IProjectObserver observer)
    {
        _observers.Remove(observer);
    }

    private async Task NotifyAssignmentChanged()
    {
        foreach (var observer in _observers)
        {
            await observer.OnAssignmentChanged(this.Id);
        }
    }
    
}