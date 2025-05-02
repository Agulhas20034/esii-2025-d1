using esii_2025_d1.Data;
using esii_2025_d1.Models;
using esii_2025_d1.Dtos.ReportDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using esii_2025_d1.Dtos.ProjectDtos;

namespace esii_2025_d1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReportController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Report
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReportsResponseDto>>> GetReports()
        {
            var reports = await _context.Reports
                .Select(report => new ReportsResponseDto
                {
                    Id = report.Id,
                    UserId = report.UserId,
                    ProjectId = report.ProjectId,
                    CreatedAt = report.CreatedAt,
                    UpdatedAt = report.UpdatedAt,
                    DeletedAt = report.DeletedAt
                })
                .ToListAsync();

            return Ok(reports);
        }

        // GET: api/Report/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ReportsResponseDto>> GetReport(int id)
        {
            var report = await _context.Reports.FindAsync(id);

            if (report == null)
            {
                return NotFound();
            }

            var reportResponse = new ReportsResponseDto
            {
                Id = report.Id,
                UserId = report.UserId,
                ProjectId = report.ProjectId,
                CreatedAt = report.CreatedAt,
                UpdatedAt = report.UpdatedAt,
                DeletedAt = report.DeletedAt
            };

            return Ok(reportResponse);
        }

        // POST: api/Report
        [HttpPost]
        public async Task<ActionResult<ReportsResponseDto>> PostReport(ReportsCreateDto reportCreateDto)
        {
            var report = new Report
            {
                UserId = reportCreateDto.UserId,
                ProjectId = reportCreateDto.ProjectId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            _context.Reports.Add(report);
            await _context.SaveChangesAsync();

            var reportResponse = new ReportsResponseDto
            {
                Id = report.Id,
                UserId = report.UserId,
                ProjectId = report.ProjectId,
                CreatedAt = report.CreatedAt,
                UpdatedAt = report.UpdatedAt,
                DeletedAt = report.DeletedAt
            };

            return CreatedAtAction(nameof(GetReport), new { id = report.Id }, reportResponse);
        }

        
        // PUT: api/Report/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReport(int id, ReportsUpdateDto reportUpdateDto)
        {
            var report = await _context.Reports.FindAsync(id);

            if (report == null)
            {
                return NotFound();
            }

            report.UserId = reportUpdateDto.UserId ?? report.UserId;
            report.ProjectId = reportUpdateDto.ProjectId ?? report.ProjectId;
            report.UpdatedAt = DateTime.UtcNow;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Reports.Any(p => p.Id == id))
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
            var report = await _context.Reports.FindAsync(id);
            if (report == null)
            {
                return NotFound();
            }

            report.DeletedAt = DateTime.Now;
            report.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
