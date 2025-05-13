using esii_2025_d1.Dtos.AssignmentDtos;
using esii_2025_d1.Dtos.ProjectDtos;
using esii_2025_d1.Interfaces.ObserverPattern;

namespace esii_2025_d1.Controllers;

using esii_2025_d1.Data;
using esii_2025_d1.Models;
using esii_2025_d1.Dtos.ProjectDtos;
using esii_2025_d1.Models.Enums;
using esii_2025_d1.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class ProjectController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogService _logService;
    private readonly IProjectObserver _projectObserver;
    private readonly SingletonUserManager _usermanager;
    protected string Entity = "Project";
    
    public ProjectController(ApplicationDbContext context, ILogService logService,SingletonUserManager usermanager)
    {
        _context = context;
        _logService = logService;
        //_projectObserver = projectObserver;
        _usermanager = usermanager;

    }
    
    // GET: api/Project
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectResponseDto>>> GetProjects()
    {
        string? userId = await _usermanager.GetCurrentUserIdAsync();
        if (userId is null)
        {
            userId = "1";
        }
        try
        {
            var projects = await _context.Projects
                .Include(p => p.Assignments) 
                .Include(p => p.Media)
                .Include(p => p.Reports)
                .Select(project => new ProjectResponseDto
                {
                    Id = project.Id,
                    UserId = project.UserId,
                    CustomerId = project.CustomerId,
                    Name = project.Name,
                    Description = project.Description,
                    Status = project.Status,
                    DailyWorkHours = project.DailyWorkHours,
                    HourlyRate = project.HourlyRate,
                    Assignments = project.Assignments.Select(a => new AssignmentResponseDto
                    {
                        Id = a.Id,
                        UserId = a.UserId,
                        ProjectId = a.ProjectId,
                        Description = a.Description,
                        HourlyRate = a.HourlyRate,
                        StartDate = a.StartDate,
                        EndDate = a.EndDate,
                        Status = a.Status,
                    }).ToList(),
                })
                .ToListAsync();
            
            await _logService.CreateLog(new Log
            {
                entity_id = null,
                entity_name = Entity,
                user_id = userId,
                action = LogAction.Read
            });

            return Ok(projects);
        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"Error fetching Projects: {e.Message}");
            throw;
        }
    }
    
    // GET: api/Project/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectResponseDto>> GetProject(int id)
    {
        string? userId = await _usermanager.GetCurrentUserIdAsync();
        if (userId is null)
        {
            userId = "1";
        }
        var project = await _context.Projects.FindAsync(id);

        if (project == null)
        {
            return NotFound();
        }

        try
        {
            var projectResponse = new ProjectResponseDto
            {
                Id = project.Id,
                UserId = project.UserId,
                CustomerId = project.CustomerId,
                Name = project.Name,
                Description = project.Description,
                HourlyRate = project.HourlyRate,
                DailyWorkHours = project.DailyWorkHours,
                Status = project.Status,
                Assignments = project.Assignments.Select(a => new AssignmentResponseDto
                {
                    Id = a.Id,
                    UserId = a.UserId,
                    ProjectId = a.ProjectId,
                    Description = a.Description,
                    HourlyRate = a.HourlyRate,
                    StartDate = a.StartDate,
                    EndDate = a.EndDate,
                    Status = a.Status,
                }).ToList(),
            };
            
            await _logService.CreateLog(new Log
            {
                entity_id = project.Id,
                entity_name = Entity,
                user_id = userId,
                action = LogAction.Read
            });

            return Ok(projectResponse);
        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"Error fetching Project: {e.Message}");
            throw;
        }
    }
    
    // POST: api/Project
    [HttpPost]
    public async Task<ActionResult<ProjectResponseDto>> PostProject(ProjectCreateDto projectRequest)
    {
        string? userId = await _usermanager.GetCurrentUserIdAsync();
        if (userId is null)
        {
            userId = "1";
        }
        try
        {
            var project = new Project
            {
                UserId = projectRequest.UserId,
                CustomerId = projectRequest.CustomerId,
                Name = projectRequest.Name,
                Description = projectRequest.Description,
                HourlyRate = projectRequest.HourlyRate,
                DailyWorkHours = projectRequest.DailyWorkHours,
                Status = projectRequest.Status,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            await _logService.CreateLog(new Log
            {
                entity_id = project.Id,
                entity_name = Entity,
                user_id = userId,
                action = LogAction.Create
            });

            return await GetProject(project.Id);
            
        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"Error creating Project: {e.Message}");
            throw;
        }
    }
    
    // PUT: api/Project/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProject(int id, ProjectUpdateDto projectRequest)
    {
        string? userId = await _usermanager.GetCurrentUserIdAsync();
        if (userId is null)
        {
            userId = "1";
        }
        var project = await _context.Projects.FindAsync(id);

        if (project == null)
            return NotFound();
        try
        {
            project.CustomerId = projectRequest.CustomerId ?? project.CustomerId;
            project.UserId = projectRequest.UserId ?? project.UserId;
            project.Name = projectRequest.Name ?? project.Name;
            project.Description = projectRequest.Description ?? project.Description;
            project.HourlyRate = projectRequest.HourlyRate ?? project.HourlyRate;
            project.DailyWorkHours = projectRequest.DailyWorkHours ?? project.DailyWorkHours;
            project.Status = projectRequest.Status;
            project.UpdatedAt = DateTime.UtcNow;
            
            await _logService.CreateLog(new Log
            {
                entity_id = project.Id,
                entity_name = Entity,
                user_id = userId,
                action = LogAction.Update
            });

            await _context.SaveChangesAsync();
            
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Projects.Any(a => a.Id == id))
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
        string? userId = await _usermanager.GetCurrentUserIdAsync();
        if (userId is null)
        {
            userId = "1";
        }
        try
        {
            var project = await _context.Projects.FindAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            project.DeletedAt = DateTime.UtcNow;
            project.UpdatedAt = DateTime.UtcNow;

            await _logService.CreateLog(new Log
            {
                entity_id = project.Id,
                entity_name = Entity,
                user_id = userId,
                action = LogAction.Delete
            });

            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"Error deleting Project: {e.Message}");
            throw;
        }
        return NoContent();
    }
}
    




    
    
    




