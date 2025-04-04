using esii_2025_d1.Data;
using esii_2025_d1.Dtos.ProjectDtos;
using esii_2025_d1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace esii_2025_d1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Project
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectsResponseDto>>> GetProjects()
        {
            var projects = await _context.Projects
                .Select(p => new ProjectsResponseDto
                {
                    Id = p.Id,
                    UserId = p.UserId,
                    CustomerId = p.CustomerId,
                    Name = p.Name,
                    HourlyRate = p.HourlyRate,
                    DailyWorkHours = p.DailyWorkHours,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt,
                    DeletedAt = p.DeletedAt
                })
                .ToListAsync();

            return Ok(projects);
        }

        // GET: api/Project/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectsResponseDto>> GetProjectById(int id)
        {
            var project = await _context.Projects.FindAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            var projectResponse = new ProjectsResponseDto
            {
                Id = project.Id,
                UserId = project.UserId,
                CustomerId = project.CustomerId,
                Name = project.Name,
                HourlyRate = project.HourlyRate,
                DailyWorkHours = project.DailyWorkHours,
                CreatedAt = project.CreatedAt,
                UpdatedAt = project.UpdatedAt,
                DeletedAt = project.DeletedAt
            };

            return Ok(projectResponse);
        }

        // POST: api/Project
        [HttpPost]
        public async Task<ActionResult<ProjectsResponseDto>> PostProject(ProjectsCreateDto projectCreateDto)
        {
            var project = new Project
            {
                UserId = projectCreateDto.UserId,
                CustomerId = projectCreateDto.CustomerId,
                Name = projectCreateDto.Name,
                HourlyRate = projectCreateDto.HourlyRate,
                DailyWorkHours = projectCreateDto.DailyWorkHours,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            var projectResponse = new ProjectsResponseDto
            {
                Id = project.Id,
                UserId = project.UserId,
                CustomerId = project.CustomerId,
                Name = project.Name,
                HourlyRate = project.HourlyRate,
                DailyWorkHours = project.DailyWorkHours,
                CreatedAt = project.CreatedAt,
                UpdatedAt = project.UpdatedAt,
                DeletedAt = project.DeletedAt
            };

            return CreatedAtAction(nameof(GetProjectById), new { id = project.Id }, projectResponse);
        }

        // PUT: api/Project/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject(int id, ProjectsUpdateDto projectUpdateDto)
        {
            var project = await _context.Projects.FindAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            project.UserId = projectUpdateDto.UserId ?? project.UserId;
            project.CustomerId = projectUpdateDto.CustomerId ?? project.CustomerId;
            project.Name = projectUpdateDto.Name ?? project.Name;
            project.HourlyRate = projectUpdateDto.HourlyRate ?? project.HourlyRate;
            project.DailyWorkHours = projectUpdateDto.DailyWorkHours ?? project.DailyWorkHours;
            project.UpdatedAt = DateTime.UtcNow;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Projects.Any(p => p.Id == id))
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

        // DELETE: api/Project/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            project.DeletedAt = DateTime.UtcNow;
            project.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
