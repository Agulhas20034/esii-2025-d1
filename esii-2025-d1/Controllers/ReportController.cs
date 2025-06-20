using esii_2025_d1.Data;
using esii_2025_d1.Models;
using esii_2025_d1.Models.Enums;
using esii_2025_d1.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text.Json;

[ApiController]
[Route("api/[controller]")]
public class ReportController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogService _logService;
    private readonly SingletonUserManager _usermanager;
    private const string Entity = "ProjectReport";
    private string userId = "Undefined";

    public ReportController(ApplicationDbContext context, ILogService logService, SingletonUserManager usermanager,
        IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _logService = logService;
        _usermanager = usermanager;
    }

    [HttpGet("monthly")]
    public async Task<ActionResult<ReportData>> GetPersonalMonthlyReport(
        [FromQuery] int year,
        [FromQuery] int month,
        [FromQuery] bool saveReport = false,
        [FromQuery] string? userId = null) 
    {
       
        userId ??= await _usermanager.GetCurrentUserIdAsync();
    
        if (string.IsNullOrEmpty(userId))
            return BadRequest("User ID not provided and no authenticated user found.");

        var firstDayOfMonth = new DateTime(year, month, 1);
        var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

        var existingReport = await _context.Reports
            .FirstOrDefaultAsync(r => r.UserId == userId &&
                                      r.StartDate == firstDayOfMonth &&
                                      r.EndDate == lastDayOfMonth);

        if (existingReport != null && !saveReport)
        {
            return Ok(JsonSerializer.Deserialize<ReportData>(existingReport.ReportDataJson));
        }

        var reportData = await GeneratePersonalReportData(userId, firstDayOfMonth, lastDayOfMonth);

        if (saveReport)
        {
            var report = new ProjectReport
            {
                UserId = userId,
                StartDate = firstDayOfMonth,
                EndDate = lastDayOfMonth,
                Title = $"ProjectReport - {CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month)} {year}",
                TotalHours = reportData.TotalHours,
                TotalAmount = reportData.TotalAmount,
                ReportDataJson = JsonSerializer.Serialize(reportData),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Reports.Add(report);
            await _context.SaveChangesAsync();
        }

        return Ok(reportData);
    }

    private async Task<ReportData> GeneratePersonalReportData(string userId, DateTime startDate, DateTime endDate)
    {
        try
        {
            var completedAssignments = await _context.Assignments
            .Include(a => a.Project)
            .Where(a => a.UserId == userId &&
                        a.Status == AssignmentStatus.Completed &&
                        a.StartDate <= endDate &&
                        a.EndDate >= startDate)
            .ToListAsync();

            var reportData = new ReportData();

            var dailyGroups = completedAssignments
                .GroupBy(a => a.EndDate?.Date)
                .Where(g => g.Key.HasValue)
                .OrderBy(g => g.Key);

            foreach (var dayGroup in dailyGroups)
            {
                var dayReport = new DailyReport { Date = dayGroup.Key.Value };

            foreach (var assignment in dayGroup)
            {
                var hours = (assignment.EndDate - assignment.StartDate)?.TotalHours ?? 0;
                var hourlyRate = assignment.HourlyRate ?? assignment.Project?.HourlyRate;
                var amount = hourlyRate.HasValue ? (decimal)(hours * (double)hourlyRate.Value) : (decimal?)null;

                dayReport.Tasks.Add(new ReportTask
                {
                    AssignmentId = assignment.Id,
                    ProjectId = assignment.ProjectId,
                    ProjectName = assignment.Project?.Name ?? "NO Project",
                    Description = assignment.Description ?? string.Empty,
                    Hours = hours,
                    HourlyRate = hourlyRate,
                    Amount = amount
                });

                dayReport.DailyHours += hours;
                if (amount.HasValue)
                    dayReport.DailyAmount = (dayReport.DailyAmount ?? 0) + amount.Value;
            }

            var exceeded = dayGroup
                .Where(a => a.Project?.DailyWorkHours.HasValue == true)
                .GroupBy(a => a.ProjectId)
                .Any(g => g.Sum(a => (a.EndDate - a.StartDate)?.TotalHours ?? 0) >
                          (g.First().Project?.DailyWorkHours ?? 24));

            dayReport.ExceededDailyHours = exceeded;
            reportData.DailyReports.Add(dayReport);
            }

            reportData.TotalHours = reportData.DailyReports.Sum(d => d.DailyHours);
            reportData.TotalAmount = reportData.DailyReports.Sum(d => d.DailyAmount);
        
            await _logService.CreateLog(new Log
            {
                entity_id = null ,
                entity_name = Entity,
                user_id = userId,
                action = LogAction.Read
            });

            return reportData;
        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"Error generating report data: {e.Message}");
            throw;
        }
        
    }
    
    [HttpGet("list")]
    public async Task<ActionResult<List<ProjectReport>>> GetUserReports(
        [FromQuery] string userId)
    {
        if (string.IsNullOrEmpty(userId))
        {
            userId = await _usermanager.GetCurrentUserIdAsync();
        }

        try
        {
            var reports = await _context.Reports
                .Where(r => r.UserId == userId)
                .OrderByDescending(r => r.StartDate)
                .ToListAsync();
            
            await _logService.CreateLog(new Log
            {
                entity_id = null ,
                entity_name = Entity,
                user_id = userId,
                action = LogAction.Read
            });
            
            return Ok(reports);
        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"Error fetching user reports: {e.Message}");
            throw;
        }
        
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReport(int id)
    {
        
        string? userId = await _usermanager.GetCurrentUserIdAsync();
    
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized(new { message = "User not authenticated." });
        }

        try
        {
            var report = await _context.Reports
                .FirstOrDefaultAsync(r => r.Id == id);

            if (report == null)
            {
                return NotFound(new { message = "ProjectReport not found or already deleted." });
            }

            
            report.DeletedAt = DateTime.UtcNow;
            report.UpdatedAt = DateTime.UtcNow;
        
            await _logService.CreateLog(new Log
            {
                entity_id = null ,
                entity_name = Entity,
                user_id = userId,
                action = LogAction.Delete
            });
        
            await _context.SaveChangesAsync();
        
            return NoContent();
        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"Error deleting Report: {e.Message}");
            return StatusCode(500, new { message = "An error occurred while deleting the report." });
        }
    }
    
}