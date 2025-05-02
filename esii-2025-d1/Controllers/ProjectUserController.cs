using esii_2025_d1.Dtos.MediaDtos;
using esii_2025_d1.Dtos.ProjectUserDtos;

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
    
    public ProjectUserController(ApplicationDbContext context, ILogService logService)
    {
        _context = context;
        _logService = logService;
    }
    
    // GET: api/ProjectUser
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectUserResponseDto>>> GetProjectUsers()
    {
        try
        {
            var projectUsers = await _context.ProjectUsers
                .AsNoTracking()
                .Select(projectUser => new ProjectUserResponseDto
                {
                    Id = projectUser.Id,
                    ProjectId = projectUser.ProjectId,
                    UserId = projectUser.UserId,
                    InviterId = projectUser.InviterId,
                    Status = projectUser.Status
                })
                .ToListAsync();
            
            await _logService.CreateLog(new Log
            {
                entity_id = null,
                entity_name = Entity,
                user_id = 1,
                action = LogAction.Read
            });

            return Ok(projectUsers);
        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"Error fetching ProjectUsers: {e.Message}");
            throw;
        }
    }
    
    // GET: api/ProjectUser/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectUserResponseDto>> GetProjectUser(int id)
    {
        var projectUser = await _context.ProjectUsers.FindAsync(id);

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
                Status = projectUser.Status
            };
            
            await _logService.CreateLog(new Log
            {
                entity_id = projectUser.Id,
                entity_name = Entity,
                user_id = 1,
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
        try
        {
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

            await _logService.CreateLog(new Log
            {
                entity_id = projectUser.Id,
                entity_name = Entity,
                user_id = 1,
                action = LogAction.Create
            });

            return await GetProjectUser(projectUser.Id);

        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"Error creating ProjectUser: {e.Message}");
            throw;
        }
    }
    
    // PUT: api/ProjectUser/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProjectUser(int id, ProjectUserUpdateDto projectUserRequest)
    {
        var projectUser = await _context.ProjectUsers.FindAsync(id);

        if (projectUser == null)
            {
            return NotFound();
        try
        {
            projectUser.ProjectId = projectUserRequest.ProjectId ?? projectUser.ProjectId;
            projectUser.UserId = projectUserRequest.UserId ?? projectUser.UserId;
            projectUser.InviterId = projectUserRequest.InviterId ?? projectUser.InviterId;
            projectUser.Status = projectUserRequest.Status ?? projectUser.Status;
            projectUser.UpdatedAt = DateTime.UtcNow;
            
            await _logService.CreateLog(new Log
            {
                entity_id = projectUser.Id,
                entity_name = Entity,
                user_id = 1,
                action = LogAction.Update
            });

            await _context.SaveChangesAsync();
            
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.ProjectUsers.Any(a => a.Id == id))
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
        try
        {
            var projectUser = await _context.ProjectUsers.FindAsync(id);

            if (projectUser == null)
            {
                return NotFound();
            }

            projectUser.DeletedAt = DateTime.UtcNow;
            projectUser.UpdatedAt = DateTime.UtcNow;

            await _logService.CreateLog(new Log
            {
                entity_id = projectUser.Id,
                entity_name = Entity,
                user_id = 1,
                action = LogAction.Delete
            });

            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"Error deleting ProjectUser: {e.Message}");
            throw;
        }
        return NoContent();
    }
}








    
    
    

    
    
    
    
    
    



    
    
    
    
    
    
    


    

