using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace esii_2025_d1.Models;

public class ProjectReport
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    public string UserId { get; set; }

    public int? ProjectId { get; set; }
    
    [Required]
    public DateTime StartDate { get; set; }
    
    [Required]
    public DateTime EndDate { get; set; }
    
    [Required]
    public string Title { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;
    
    public double TotalHours { get; set; }
    
    public decimal? TotalAmount { get; set; }
    
    [Required]
    public string ReportDataJson { get; set; } = string.Empty; 
    
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? DeletedAt { get; set; }
    
    // Relacionamentos
    public virtual Project? Project { get; set; }
}

// Classes to represent the report data structure
public class ReportData
{
    public List<DailyReport> DailyReports { get; set; } = new();
    public double TotalHours { get; set; }
    public decimal? TotalAmount { get; set; }
}

public class DailyReport
{
    public DateTime Date { get; set; }
    public List<ReportTask> Tasks { get; set; } = new();
    public double DailyHours { get; set; }
    public decimal? DailyAmount { get; set; }
    public bool ExceededDailyHours { get; set; }
}

public class ReportTask
{
    public int? AssignmentId { get; set; }
    public int? ProjectId { get; set; }
    public string ProjectName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double Hours { get; set; }
    public float? HourlyRate { get; set; }
    public decimal? Amount { get; set; }
}