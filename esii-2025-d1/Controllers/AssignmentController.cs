using esii_2025_d1.Data;
using esii_2025_d1.Models;
using esii_2025_d1.Dtos.AssignmentsDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace esii_2025_d1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AssignmentController(ApplicationDbContext context)
        {
            _context = context;
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

            return Ok(assignments);
        }

        // GET: api/Assignment/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AssignmentResponseDto>> GetAssignment(int id)
        {
            var assignment = await _context.Assignments.FindAsync(id);

            if (assignment == null)
                return NotFound();

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
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Status = dto.Status,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Assignments.Add(assignment);
            await _context.SaveChangesAsync();

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
            var assignment = await _context.Assignments.FindAsync(id);

            if (assignment == null)
                return NotFound();

            assignment.UserId = dto.UserId; // ?? assignment.UserId;
            assignment.ProjectId = dto.ProjectId; // ?? assignment.ProjectId;
            assignment.Description = dto.Description ?? assignment.Description;
            assignment.HourlyRate = dto.HourlyRate ?? assignment.HourlyRate;
            assignment.StartDate = dto.StartDate ?? assignment.StartDate;
            assignment.EndDate = dto.EndDate ?? assignment.EndDate;
            assignment.Status = dto.Status ?? assignment.Status;
            assignment.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Assignment/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssignment(int id)
        {
            var assignment = await _context.Assignments.FindAsync(id);

            if (assignment == null)
                return NotFound();

            assignment.DeletedAt = DateTime.UtcNow;
            assignment.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
