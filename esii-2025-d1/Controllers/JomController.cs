using esii_2025_d1.Data;
using esii_2025_d1.Dtos.JomDtos;
using esii_2025_d1.Models;
using esii_2025_d1.Models.Enums;
using esii_2025_d1.Services;

namespace esii_2025_d1.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class JomController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogService _logService;
    protected string Entity = "Jom";
    private readonly SingletonUserManager _usermanager;

    public JomController(
        ApplicationDbContext context,
        ILogService logService,
        SingletonUserManager usermanager
        )
    {
        _context = context;
        _logService = logService;
        _usermanager = usermanager;

    }
    
    // GET: api/Jom
    [HttpGet]
    public async Task<ActionResult<IEnumerable<JomResponseDto>>> GetJoms()
    {
        string? userId = await _usermanager.GetCurrentUserIdAsync();
        if (userId is null)
        {
            userId = "1";
        }
        try
        {
            var joms = await _context.Joms
                .Select(jom => new JomResponseDto
                {
                    Id = jom.Id,
                    Label = jom.Label,
                    Date = jom.Date,
                    IsDone = jom.IsDone,
                    TestNumber = jom.TestNumber,
                    created_at = jom.created_at,
                    updated_at = jom.updated_at
                })
                .ToListAsync();
            
            await _logService.CreateLog(new Log
            {
                entity_id = null,
                entity_name = Entity,
                user_id = userId,
                action = LogAction.Read
            });
            
            return Ok(joms);
        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"Error fetching Joms: {e.Message}");
            throw;
        }
    }
    
    // GET: api/Jom/"id"
    [HttpGet("{id}")]
    public async Task<ActionResult<JomResponseDto>> GetJom(int id)
    {
        string? userId = await _usermanager.GetCurrentUserIdAsync();
        if (userId is null)
        {
            userId = "1";
        }
        var jom = await _context.Joms.FindAsync(id);
    
        if (jom == null)
        {
            return NotFound();
        }

        var jomResponse = new JomResponseDto
        {
            Id = jom.Id,
            Label = jom.Label,
            Date = jom.Date,
            IsDone = jom.IsDone,
            TestNumber = jom.TestNumber,
            created_at = jom.created_at,
            updated_at = jom.updated_at
        };
        
        await _logService.CreateLog(new Log
        {
            entity_id = null,
            entity_name = Entity,
            user_id = userId,
            action = LogAction.Read
        });

        return Ok(jomResponse);
    }

    // POST: api/Jom
    [HttpPost]
    public async Task<ActionResult<JomCreateDto>> PostJom(JomCreateDto jomRequest)
    {
        string? userId = await _usermanager.GetCurrentUserIdAsync();
        if (userId is null)
        {
            userId = "1";
        }
        var jom = new Jom
        {
            Label = jomRequest.Label,
            Date = jomRequest.Date ?? DateTime.UtcNow,
            IsDone = jomRequest.IsDone,
            TestNumber = jomRequest.TestNumber,
            created_at = DateTime.UtcNow,
            updated_at = DateTime.UtcNow,
        };
        
        await _logService.CreateLog(new Log
        {
            entity_id = null,
            entity_name = Entity,
            user_id = userId,
            action = LogAction.Create
        });
        
        _context.Joms.Add(jom);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetJom), new { id = jom.Id }, jom);
    }
        
    // PUT: api/Jom/"id"
    [HttpPut("{id}")]
    public async Task<IActionResult> PutJom(int id, JomUpdateDto jom)
    {
        string? userId = await _usermanager.GetCurrentUserIdAsync();
        if (userId is null)
        {
            userId = "1";
        }
        var existingJom = await _context.Joms.FindAsync(id);
        
        if (existingJom == null)
        {
            return NotFound();
        }
        
        // Update only the modified properties
        existingJom.Label = jom.Label ?? existingJom.Label;
        existingJom.Date = jom.Date != default ? jom.Date : existingJom.Date;
        existingJom.IsDone = jom.IsDone;
        existingJom.TestNumber = existingJom.TestNumber != jom.TestNumber ? jom.TestNumber : existingJom.TestNumber;
        existingJom.updated_at = DateTime.UtcNow;
        
        try
        {
            
            await _logService.CreateLog(new Log
            {
                entity_id = null,
                entity_name = Entity,
                user_id = userId,
                action = LogAction.Update
            });
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Joms.Any(e => e.Id == id))
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
    
    // DELETE: api/Jom/"id"
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteJom(int id)
    {
        string? userId = await _usermanager.GetCurrentUserIdAsync();
        if (userId is null)
        {
            userId = "1";
        }
        var jom = await _context.Joms.FindAsync(id);
        if (jom == null)
        {
            return NotFound();
        }
        
        jom.updated_at = DateTime.UtcNow;
        jom.deleted_at = DateTime.UtcNow;
        
        await _logService.CreateLog(new Log
        {
            entity_id = null,
            entity_name = Entity,
            user_id = userId,
            action = LogAction.Delete
        });
        
        await _context.SaveChangesAsync();
        return NoContent();
    }
}


