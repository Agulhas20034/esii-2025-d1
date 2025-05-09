using esii_2025_d1.Data;
using esii_2025_d1.Models;
using esii_2025_d1.Dtos.AssignmentsDtos;
using esii_2025_d1.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace esii_2025_d1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AssignmentController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogService _logService;
    protected string entity = "Assignment";

    public AssignmentController(ApplicationDbContext context, ILogService logService)
    {
        _context = context;
        _logService = logService;
    }

    // GET: api/Assignment
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AssignmentResponseDto>>> GetAssignments()
    {
        var assignments = await _context.Assignments
            .Select(a => new AssignmentResponseDto
            {
                Id = a.Id,
                UserId = a.UserId,
                ProjectId = a.ProjectId,
                Description = a.Description,
                HourlyRate = a.HourlyRate,
                StartDate = a.StartDate,
                EndDate = a.EndDate,
                Status = a.Status,
                CreatedAt = a.CreatedAt,
                UpdatedAt = a.UpdatedAt,
                DeletedAt = a.DeletedAt
            })
            .ToListAsync();

        await _logService.CreateLog(new Log
        {
            entity_id = null,
            entity_name = entity,
            user_id = 1,
            action = "GetList",
        });

        return Ok(assignments);
    }

    // GET: api/Assignment/5
    [HttpGet("{id}")]
    public async Task<ActionResult<AssignmentResponseDto>> GetAssignment(int id)
    {
        var a = await _context.Assignments.FindAsync(id);

        if (a == null)
            return NotFound();

        var response = new AssignmentResponseDto
        {
            Id = a.Id,
            UserId = a.UserId,
            ProjectId = a.ProjectId,
            Description = a.Description,
            HourlyRate = a.HourlyRate,
            StartDate = a.StartDate,
            EndDate = a.EndDate,
            Status = a.Status,
            CreatedAt = a.CreatedAt,
            UpdatedAt = a.UpdatedAt,
            DeletedAt = a.DeletedAt
        };

        await _logService.CreateLog(new Log
        {
            entity_id = a.Id,
            entity_name = entity,
            user_id = 1,
            action = "GetID",
        });

        return Ok(response);
    }

    // POST: api/Assignment
    [HttpPost]
    public async Task<ActionResult<AssignmentResponseDto>> PostAssignment(AssignmentCreateDto dto)
    {
        var assignment = new Assignment
        {
            UserId = dto.UserId,
            ProjectId = dto.ProjectId,
            Description = dto.Description,
            HourlyRate = dto.HourlyRate,
            StartDate = dto.StartDate.HasValue ? dto.StartDate.Value.ToUniversalTime() : DateTime.UtcNow,
            EndDate = dto.EndDate.HasValue ? dto.EndDate.Value.ToUniversalTime() : DateTime.UtcNow.AddHours(1),
            Status = dto.Status,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Assignments.Add(assignment);
        await _context.SaveChangesAsync();

        await _logService.CreateLog(new Log
        {
            entity_id = assignment.Id,
            entity_name = entity,
            user_id = 1,
            action = "Post",
        });

        var response = new AssignmentResponseDto
        {
            Id = assignment.Id,
            UserId = assignment.UserId,
            ProjectId = assignment.ProjectId,
            Description = assignment.Description,
            HourlyRate = assignment.HourlyRate,
            StartDate = assignment.StartDate,
            EndDate = assignment.EndDate,
            Status = assignment.Status,
            CreatedAt = assignment.CreatedAt,
            UpdatedAt = assignment.UpdatedAt,
            DeletedAt = assignment.DeletedAt
        };

        return CreatedAtAction(nameof(GetAssignment), new { id = assignment.Id }, response);
    }

    // PUT: api/Assignment/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAssignment(int id, AssignmentUpdateDto dto)
    {
        var a = await _context.Assignments.FindAsync(id);

        if (a == null)
            return NotFound();

        a.UserId = dto.UserId;
        a.ProjectId = dto.ProjectId;
        a.Description = dto.Description ?? a.Description;
        a.HourlyRate = dto.HourlyRate ?? a.HourlyRate;
        a.StartDate = dto.StartDate.HasValue ? dto.StartDate.Value.ToUniversalTime() : a.StartDate;
        a.EndDate = dto.EndDate.HasValue ? dto.EndDate.Value.ToUniversalTime() : a.EndDate;
        a.Status = dto.Status ?? a.Status;
        a.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        await _logService.CreateLog(new Log
        {
            entity_id = a.Id,
            entity_name = entity,
            user_id = 1,
            action = "Update",
        });

        return NoContent();
    }

    // DELETE: api/Assignment/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAssignment(int id)
    {
        var a = await _context.Assignments.FindAsync(id);

        if (a == null)
            return NotFound();

        a.DeletedAt = DateTime.UtcNow;
        a.UpdatedAt = DateTime.UtcNow;

        await _logService.CreateLog(new Log
        {
            entity_id = a.Id,
            entity_name = entity,
            user_id = 1,
            action = "Delete",
        });

        await _context.SaveChangesAsync();

        return NoContent();
    }
}
