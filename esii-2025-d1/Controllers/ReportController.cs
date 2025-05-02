using esii_2025_d1.Dtos.MediaDtos;

namespace esii_2025_d1.Controllers;


using esii_2025_d1.Data;
using esii_2025_d1.Models;
using esii_2025_d1.Dtos.ReportDtos;
using esii_2025_d1.Models.Enums;
using esii_2025_d1.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using esii_2025_d1.Dtos.ProjectDtos;

[ApiController]
[Route("api/[controller]")]
    [ApiController]
public class ReportController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogService _logService;
    protected string Entity = "Report";
    
    public ReportController(ApplicationDbContext context, ILogService logService)
    {
        _context = context;
        _logService = logService;
    }
    
    // GET: api/Report
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReportResponseDto>>> GetReports()
    {
        try
        {
            var reports = await _context.Reports
                .AsNoTracking()
                .Select(report => new ReportResponseDto
                {
                    Id = report.Id,
                    UserId = report.UserId,
                    ProjectId = report.ProjectId,
                    Media = report.Media.Select(a => new MediaResponseDto
                    {
                        Id = a.Id,
                        ProjectId = a.ProjectId,
                        ReportId = a.ReportId,
                        Name = a.Name,
                        Type = a.Type,
                        Path = a.Path,
                    }).ToList(),
                    
                })
                .ToListAsync();
            
            await _logService.CreateLog(new Log
            {
                entity_id = null,
                entity_name = Entity,
                user_id = 1,
                action = LogAction.Read
            });

            return Ok(reports);
        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"Error fetching Reports: {e.Message}");
            throw;
        }
    }
    
    // GET: api/Report/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<ReportResponseDto>> GetReport(int id)
    {
        var report = await _context.Reports
            .Include(r => r.Media)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (report == null)
        {
            return NotFound();
        }

        try
        {
            var reportResponse = new ReportResponseDto
            {
                Id = report.Id,
                UserId = report.UserId,
                ProjectId = report.ProjectId,
                Media = report.Media.Select(a => new MediaResponseDto
                {
                    Id = a.Id,
                    ProjectId = a.ProjectId,
                    ReportId = a.ReportId,
                    Name = a.Name,
                    Type = a.Type,
                    Path = a.Path,
                }).ToList(),
            };
            
            await _logService.CreateLog(new Log
            {
                entity_id = report.Id,
                entity_name = Entity,
                user_id = 1,
                action = LogAction.Read
            });

            return Ok(reportResponse);
        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"Error fetching Report: {e.Message}");
            throw;
        }
    }
    
    // POST: api/Report
    [HttpPost]
    public async Task<ActionResult<ReportResponseDto>> PostReport(ReportCreateDto reportRequest)
    {
        try
        {
            var report = new Report
            {
                UserId = reportRequest.UserId,
                ProjectId = reportRequest.ProjectId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            
            // Adiciona Media (se existirem IDs)
            if (reportRequest.MediaIds != null && reportRequest.MediaIds.Any())
            {
                var media = await _context.Media
                    .Where(m => reportRequest.MediaIds.Contains(m.Id))
                    .ToListAsync();

                report.Media = media;
            }
            
            _context.Reports.Add(report);
            await _context.SaveChangesAsync();

            await _logService.CreateLog(new Log
            {
                entity_id = report.Id,
                entity_name = Entity,
                user_id = 1,
                action = LogAction.Create
            });

            return await GetReport(report.Id);

        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"Error creating Report: {e.Message}");
            throw;
        }
    }
    
        
    // PUT: api/Report/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutReport(int id, ReportUpdateDto reportRequest)
    {
        var report = await _context.Reports
            .Include(r => r.Media) 
            .FirstOrDefaultAsync(r => r.Id == id);

        if (report == null)
            return NotFound();
        try
        {
            report.ProjectId = reportRequest.ProjectId ?? report.ProjectId;
            report.UserId = reportRequest.UserId ?? report.UserId;
            
            if (reportRequest.MediaIds != null && reportRequest.MediaIds.Any())
            {
                var newMedia = await _context.Media
                    .Where(m => reportRequest.MediaIds.Contains(m.Id))
                    .ToListAsync();
                
                if (newMedia.Count != reportRequest.MediaIds.Count)
                {
                    var missingIds = reportRequest.MediaIds.Except(newMedia.Select(m => m.Id));
                    return BadRequest($"the media id does not exist: {string.Join(", ", missingIds)}");
                }
                
                report.Media = newMedia;
            }
            report.UpdatedAt = DateTime.UtcNow;
            
            await _logService.CreateLog(new Log
            {
                entity_id = report.Id,
                entity_name = Entity,
                user_id = 1,
                action = LogAction.Update
            });

            await _context.SaveChangesAsync();
            
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Reports.Any(a => a.Id == id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE: api/Report/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReport(int id)
    {
        try
        {
            var report = await _context.Reports.FindAsync(id);

            if (report == null)
            {
                return NotFound();
            }

            report.DeletedAt = DateTime.UtcNow;
            report.UpdatedAt = DateTime.UtcNow;

            await _logService.CreateLog(new Log
            {
                entity_id = report.Id,
                entity_name = Entity,
                user_id = 1,
                action = LogAction.Delete
            });

            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"Error deleting Report: {e.Message}");
            throw;
        }
        return NoContent();
    }
    
}


    
    
    
    
    
    
    


    

