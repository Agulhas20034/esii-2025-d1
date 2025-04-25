using esii_2025_d1.Data;
using esii_2025_d1.Models;
using esii_2025_d1.Dtos.AssignmentDtos;
using esii_2025_d1.Models.Enums;
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
    protected string Entity = "Assignment";
    
    public AssignmentController(ApplicationDbContext context, ILogService logService)
    {
        _context = context;
        _logService = logService;
    }
    // GET: api/Assignment
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AssignmentResponseDto>>> GetAssignments()
    {
        try
        {
            var assignments = await _context.Assignments
                .Select(assignment => new AssignmentResponseDto
                {
                    Id = assignment.Id,
                    UserId = assignment.UserId,
                    ProjectId = assignment.ProjectId,
                    Description = assignment.Description,
                    HourlyRate = assignment.HourlyRate,
                    StartDate = assignment.StartDate,
                    EndDate = assignment.EndDate,
                    Status = assignment.Status,
                })
                .ToListAsync();
            
            await _logService.CreateLog(new Log
            {
                entity_id = null,
                entity_name = Entity,
                user_id = 1,
                action = LogAction.Read
            });

            return Ok(assignments);
        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"Error fetching Assignments: {e.Message}");
            throw;
        }
    }
    

    // GET: api/Assignment/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<AssignmentResponseDto>> GetAssignment(int id)
    {
        var assignment = await _context.Assignments.FindAsync(id);

        if (assignment == null)
        {
            return NotFound();
        }

        try
        {
            var assignmentResponse = new AssignmentResponseDto
            {
                Id = assignment.Id,
                UserId = assignment.UserId,
                ProjectId = assignment.ProjectId,
                Description = assignment.Description,
                HourlyRate = assignment.HourlyRate,
                StartDate = assignment.StartDate,
                EndDate = assignment.EndDate,
                Status = assignment.Status,
            };
            
            await _logService.CreateLog(new Log
            {
                entity_id = assignment.Id,
                entity_name = Entity,
                user_id = 1,
                action = LogAction.Read
            });

            return Ok(assignmentResponse);
        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"Error fetching Assignment: {e.Message}");
            throw;
        }
    }

    // POST: api/Assignment
    [HttpPost]
    public async Task<ActionResult<AssignmentResponseDto>> PostAssignment(AssignmentCreateDto assignmentRequest)
    {
        try
        {
            var assignment = new Assignment
            {
                ProjectId = assignmentRequest.ProjectId,
                Description = assignmentRequest.Description,
                HourlyRate = assignmentRequest.HourlyRate,
                StartDate = assignmentRequest.StartDate,
                EndDate = assignmentRequest.EndDate,
                Status = assignmentRequest.Status,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Assignments.Add(assignment);
            await _context.SaveChangesAsync();

            await _logService.CreateLog(new Log
            {
                entity_id = assignment.Id,
                entity_name = Entity,
                user_id = 1,
                action = LogAction.Create
            });

            return await GetAssignment(assignment.Id);

        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"Error creating Assignment: {e.Message}");
            throw;
        }
    }
    // PUT: api/Assignment/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAssignment(int id, AssignmentUpdateDto assignmentRequest)
    {
        var assignment = await _context.Assignments.FindAsync(id);

        if (assignment == null)
            return NotFound();
        try
        {
            assignment.ProjectId = assignmentRequest.ProjectId ?? assignment.ProjectId;
            assignment.Description = assignmentRequest.Description ?? assignment.Description;
            assignment.HourlyRate = assignmentRequest.HourlyRate ?? assignment.HourlyRate;
            assignment.StartDate = assignmentRequest.StartDate ?? assignment.StartDate;
            assignment.EndDate = assignmentRequest.EndDate ?? assignment.EndDate;
            assignment.Status = assignmentRequest.Status;
            assignment.UpdatedAt = DateTime.UtcNow;
            
            await _logService.CreateLog(new Log
            {
                entity_id = assignment.Id,
                entity_name = Entity,
                user_id = 1,
                action = LogAction.Update
            });

            await _context.SaveChangesAsync();
            
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Assignments.Any(a => a.Id == id))
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

    // DELETE: api/Assignment/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAssignment(int id)
    {
        try
        {
            var assignment = await _context.Assignments.FindAsync(id);

            if (assignment == null)
            {
                return NotFound();
            }

            assignment.DeletedAt = DateTime.UtcNow;
            assignment.UpdatedAt = DateTime.UtcNow;

            await _logService.CreateLog(new Log
            {
                entity_id = assignment.Id,
                entity_name = Entity,
                user_id = 1,
                action = LogAction.Delete
            });

            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"Error deleting Assignment: {e.Message}");
            throw;
        }
        return NoContent();
    }
}
