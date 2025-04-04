using esii_2025_d1.Data;
using esii_2025_d1.Dtos.ProjectUserDtos;
using esii_2025_d1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace esii_2025_d1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectUserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProjectUserController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ProjectUser
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectUsersResponseDto>>> GetProjectUsers()
        {
            var projectUsers = await _context.ProjectUsers
                .Select(pu => new ProjectUsersResponseDto
                {
                    Id = pu.Id,
                    ProjectId = pu.ProjectId,
                    UserId = pu.UserId,
                    InviterId = pu.InviterId,
                    Status = pu.Status,
                    CreatedAt = pu.CreatedAt,
                    UpdatedAt = pu.UpdatedAt,
                    DeletedAt = pu.DeletedAt
                })
                .ToListAsync();

            return Ok(projectUsers);
        }

        // GET: api/ProjectUser/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectUsersResponseDto>> GetProjectUserById(int id)
        {
            var projectUser = await _context.ProjectUsers.FindAsync(id);

            if (projectUser == null)
            {
                return NotFound();
            }

            var projectUserResponse = new ProjectUsersResponseDto
            {
                Id = projectUser.Id,
                ProjectId = projectUser.ProjectId,
                UserId = projectUser.UserId,
                InviterId = projectUser.InviterId,
                Status = projectUser.Status,
                CreatedAt = projectUser.CreatedAt,
                UpdatedAt = projectUser.UpdatedAt,
                DeletedAt = projectUser.DeletedAt
            };

            return Ok(projectUserResponse);
        }

        // POST: api/ProjectUser
        [HttpPost]
        public async Task<ActionResult<ProjectUsersResponseDto>> PostProjectUser(ProjectUsersCreateDto projectUserCreateDto)
        {
            var projectUser = new ProjectUser
            {
                ProjectId = projectUserCreateDto.ProjectId,
                UserId = projectUserCreateDto.UserId,
                InviterId = projectUserCreateDto.InviterId,
                Status = projectUserCreateDto.Status,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.ProjectUsers.Add(projectUser);
            await _context.SaveChangesAsync();

            var projectUserResponse = new ProjectUsersResponseDto
            {
                Id = projectUser.Id,
                ProjectId = projectUser.ProjectId,
                UserId = projectUser.UserId,
                InviterId = projectUser.InviterId,
                Status = projectUser.Status,
                CreatedAt = projectUser.CreatedAt,
                UpdatedAt = projectUser.UpdatedAt,
                DeletedAt = projectUser.DeletedAt
            };

            return CreatedAtAction(nameof(GetProjectUserById), new { id = projectUser.Id }, projectUserResponse);
        }

        // PUT: api/ProjectUser/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProjectUser(int id, ProjectUsersUpdateDto projectUserUpdateDto)
        {
            var projectUser = await _context.ProjectUsers.FindAsync(id);

            if (projectUser == null)
            {
                return NotFound();
            }

            projectUser.ProjectId = projectUserUpdateDto.ProjectId ?? projectUser.ProjectId;
            projectUser.UserId = projectUserUpdateDto.UserId ?? projectUser.UserId;
            projectUser.InviterId = projectUserUpdateDto.InviterId ?? projectUser.InviterId;
            projectUser.Status = projectUserUpdateDto.Status ?? projectUser.Status;
            projectUser.UpdatedAt = DateTime.UtcNow;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.ProjectUsers.Any(pu => pu.Id == id))
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

        // DELETE: api/ProjectUser/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjectUser(int id)
        {
            var projectUser = await _context.ProjectUsers.FindAsync(id);

            if (projectUser == null)
            {
                return NotFound();
            }

            projectUser.DeletedAt = DateTime.UtcNow;
            projectUser.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
