using esii_2025_d1.Data;
using esii_2025_d1.Models;
using esii_2025_d1.Dtos.RoleDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using esii_2025_d1.Dtos.ReportDtos;
using esii_2025_d1.Dtos.TasksDtos;

namespace esii_2025_d1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TasksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Task
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TasksResponseDto>>> GetTasks()
        {
            var task = await _context.Tasks
                .Select(role => new TasksResponseDto
                {
                    Id = role.Id,
                    PermissionId = role.PermissionId,
                    Name = role.Name,
                    CreatedAt = role.CreatedAt,
                    UpdatedAt = role.UpdatedAt,
                    DeletedAt = role.DeletedAt
                })
                .ToListAsync();

            return Ok(task);
        }

        // GET: api/Task/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TasksResponseDto>> GetTask(int id)
        {
            var task = await _context.Roles.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            var taskResponse = new TasksResponseDto
            {
                Id = task.Id,
                PermissionId = task.PermissionId,
                Name = task.Name,
                CreatedAt = task.CreatedAt,
                UpdatedAt = task.UpdatedAt,
                DeletedAt = task.DeletedAt
            };

            return Ok(taskResponse);
        }

        // POST: api/Task
        [HttpPost]
        public async Task<ActionResult<TasksResponseDto>> PostRole(TasksCreateDto taskCreateDto)
        {
            var task = new Tasks
            {
                PermissionId = taskCreateDto.PermissionId,
                Name = taskCreateDto.Name,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            var taskResponse = new TasksResponseDto
            {
                Id = task.Id,
                PermissionId = task.PermissionId,
                Name = task.Name,
                CreatedAt = task.CreatedAt,
                UpdatedAt = task.UpdatedAt,
                DeletedAt = task.DeletedAt
            };

            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, taskResponse);
        }

        // PUT: api/Task/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTask(int id, TasksUpdateDto taskUpdateDto)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            task.PermissionId = taskUpdateDto.PermissionId ?? task.PermissionId;
            task.Name = taskUpdateDto.Name ?? task.Name;
            task.UpdatedAt = DateTime.UtcNow;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Tasks.Any(p => p.Id == id))
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

        // DELETE: api/Role/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            role.DeletedAt = DateTime.Now;
            role.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
