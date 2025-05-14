namespace esii_2025_d1.Controllers;

using esii_2025_d1.Data;
using esii_2025_d1.Models;
using esii_2025_d1.Dtos.ProjectUserDtos;
using esii_2025_d1.Models.Enums;
using esii_2025_d1.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
    [ApiController]
public class ProjectUserController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogService _logService;
    protected string Entity = "ProjectUser";
    private readonly SingletonUserManager _usermanager;


    public ProjectUserController(ApplicationDbContext context, ILogService logService, SingletonUserManager usermanager)
    {
        _context = context;
        _logService = logService;
        _usermanager = usermanager;
    }

    // GET: api/ProjectUser
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectUserResponseDto>>> GetProjectUsers()
    {
        string? userId = await _usermanager.GetCurrentUserIdAsync();
        if (userId is null)
        {
            userId = "1";
        }

        try
        {
            var projectUsers = await _context.ProjectUsers
                .AsNoTracking()
                .Where(pu => pu.DeletedAt == null)
                .Select(projectUser => new ProjectUserResponseDto()
                {
                    Id = projectUser.Id,
                    ProjectId = projectUser.ProjectId,
                    UserId = projectUser.UserId,
                    InviterId = projectUser.InviterId,
                    Status = projectUser.Status,
                    CreatedAt = projectUser.CreatedAt,
                    UpdatedAt = projectUser.UpdatedAt
                })
                .ToListAsync();

            await _logService.CreateLog(new Log
            {
                entity_id = null,
                entity_name = Entity,
                user_id = userId,
                action = LogAction.Read
            });

            return Ok(projectUsers);
        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"Error fetching ProjectUsers: {e.Message}");
            return StatusCode(500, "Internal server error");
        }
    }

    // GET: api/ProjectUser/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectUserResponseDto>> GetProjectUser(int id)
    {
        string? userId = await _usermanager.GetCurrentUserIdAsync();
        if (userId is null)
        {
            userId = "1";
        }

        var projectUser = await _context.ProjectUsers
            .AsNoTracking()
            .FirstOrDefaultAsync(pu => pu.Id == id && pu.DeletedAt == null);

        if (projectUser == null)
        {
            return NotFound();
        }

        try
        {
            var projectUserResponse = new ProjectUserResponseDto
            {
                Id = projectUser.Id,
                ProjectId = projectUser.ProjectId,
                UserId = projectUser.UserId,
                InviterId = projectUser.InviterId,
                Status = projectUser.Status,
                CreatedAt = projectUser.CreatedAt,
                UpdatedAt = projectUser.UpdatedAt
            };

            await _logService.CreateLog(new Log
            {
                entity_id = projectUser.Id,
                entity_name = Entity,
                user_id = userId,
                action = LogAction.Read
            });

            return Ok(projectUserResponse);
        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"Error fetching ProjectUser: {e.Message}");
            throw;
        }
    }

    // POST: api/ProjectUser
    [HttpPost]
    public async Task<ActionResult<ProjectUserResponseDto>> PostProjectUser(ProjectUserCreateDto projectUserRequest)
    {
        string? userId = await _usermanager.GetCurrentUserIdAsync();
        if (userId is null)
        {
            userId = "1";
        }

        try
        {

            var userExists = await _context.Users.AnyAsync(u => u.Id == projectUserRequest.UserId);
            var inviterExists = await _context.Users.AnyAsync(u => u.Id == projectUserRequest.InviterId);
            var projectExists = await _context.Projects.AnyAsync(p => p.Id == projectUserRequest.ProjectId);

            if (!userExists || !projectExists)
            {
                return BadRequest("User, inviter or project not found");
            }

            /*if (!userExists || !inviterExists || !projectExists)
            {
                return BadRequest("User, inviter or project not found");
            }*/

            var projectUser = new ProjectUser
            {
                UserId = projectUserRequest.UserId,
                ProjectId = projectUserRequest.ProjectId,
                InviterId = projectUserRequest.InviterId,
                Status = projectUserRequest.Status,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.ProjectUsers.Add(projectUser);
            await _context.SaveChangesAsync();

            var response = new ProjectUserResponseDto
            {
                Id = projectUser.Id,
                ProjectId = projectUser.ProjectId,
                UserId = projectUser.UserId,
                InviterId = projectUser.InviterId,
                Status = projectUser.Status,
                CreatedAt = projectUser.CreatedAt,
                UpdatedAt = projectUser.UpdatedAt
            };

            await _logService.CreateLog(new Log
            {
                entity_id = projectUser.Id,
                entity_name = Entity,
                user_id = userId,
                action = LogAction.Create
            });

            return CreatedAtAction(nameof(GetProjectUser), new { id = projectUser.Id }, response);
        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"Error creating ProjectUser: {e.Message}");
            return StatusCode(500, "Internal server error");
        }
    }

    // PUT: api/ProjectUser/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProjectUser(int id, ProjectUserUpdateDto projectUserRequest)
    {
        string? userId = await _usermanager.GetCurrentUserIdAsync();
        if (userId is null)
        {
            userId = "1";
        }

        var projectUser = await _context.ProjectUsers
            .FirstOrDefaultAsync(pu => pu.Id == id && pu.DeletedAt == null);

        if (projectUser == null)
            {
            return NotFound();

        try
        {
            if (projectUserRequest.ProjectId.HasValue)
            {
                if (!await _context.Projects.AnyAsync(p => p.Id == projectUserRequest.ProjectId.Value))
                    return BadRequest("Project not found");

                projectUser.ProjectId = projectUserRequest.ProjectId.Value;
            }

            if (!string.IsNullOrEmpty(projectUserRequest.UserId))
            {
                if (!await _context.Users.AnyAsync(u => u.Id == projectUserRequest.UserId))
                    return BadRequest("User not found");

                projectUser.UserId = projectUserRequest.UserId;
            }

            if (!string.IsNullOrEmpty(projectUserRequest.InviterId))
            {
                if (!await _context.Users.AnyAsync(u => u.Id == projectUserRequest.InviterId))
                    return BadRequest("Inviter not found");

                projectUser.InviterId = projectUserRequest.InviterId;
            }

            if (projectUserRequest.Status.HasValue)
                projectUser.Status = projectUserRequest.Status.Value;

            projectUser.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            await _logService.CreateLog(new Log
            {
                entity_id = projectUser.Id,
                entity_name = Entity,
                user_id = userId,
                action = LogAction.Update
            });

            return NoContent();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.ProjectUsers.Any(a => a.Id == id))
            {
                return NotFound();
            }

            throw;
        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"Error updating ProjectUser: {e.Message}");
            return StatusCode(500, "Internal server error");
        }
<<<<<<< HEAD

        return NoContent();
=======
>>>>>>> origin/develop
    }

    // DELETE: api/ProjectUser/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProjectUser(int id)
    {
        string? userId = await _usermanager.GetCurrentUserIdAsync();
        if (userId is null)
        {
            userId = "1";
        }

        try
        {
            var projectUser = await _context.ProjectUsers.FindAsync(id);

            if (projectUser == null || projectUser.DeletedAt != null)
            {
                return NotFound();
            }

            projectUser.DeletedAt = DateTime.UtcNow;
            projectUser.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            await _logService.CreateLog(new Log
            {
                entity_id = projectUser.Id,
                entity_name = Entity,
                user_id = userId,
                action = LogAction.Delete
            });

            return NoContent();
        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"Error deleting ProjectUser: {e.Message}");
            return StatusCode(500, "Internal server error");
        }
    }
}








    
    
    

    
    
    
    
    
    



    
    
    
    
    
    
    


    

