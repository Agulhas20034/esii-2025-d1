namespace esii_2025_d1.Controllers;

using esii_2025_d1.Data;
using esii_2025_d1.Models;
using esii_2025_d1.Dtos.MediaDtos;
using esii_2025_d1.Models.Enums;
using esii_2025_d1.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class MediaController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogService _logService;
    protected string Entity = "Media";
    
    public MediaController(ApplicationDbContext context, ILogService logService)
    {
        _context = context;
        _logService = logService;
    }
    
    // GET: api/Media
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MediaResponseDto>>> GetMedias()
    {
        try
        {
            var medias = await _context.Media
                .Select(media => new MediaResponseDto
                {
                    Id = media.Id,
                    ProjectId = media.ProjectId,
                    ReportId = media.ReportId,
                    Name = media.Name,
                    Type = media.Type,
                    Path = media.Path,
                })
                .ToListAsync();
            
            await _logService.CreateLog(new Log
            {
                entity_id = null,
                entity_name = Entity,
                user_id = 1,
                action = LogAction.Read
            });

            return Ok(medias);
        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"Error fetching Medias: {e.Message}");
            throw;
        }
    }
    
    // GET: api/Media/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<MediaResponseDto>> GetMedia(int id)
    {
        var media = await _context.Media.FindAsync(id);

        if (media == null)
        {
            return NotFound();
        }

        try
        {
            var mediaResponse = new MediaResponseDto
            {
                Id = media.Id,
                ProjectId = media.ProjectId,
                ReportId = media.ReportId,
                Name = media.Name,
                Type = media.Type,
                Path = media.Path,
            };
            
            await _logService.CreateLog(new Log
            {
                entity_id = media.Id,
                entity_name = Entity,
                user_id = 1,
                action = LogAction.Read
            });

            return Ok(mediaResponse);
        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"Error fetching Media: {e.Message}");
            throw;
        }
    }
    
    // POST: api/Media
    [HttpPost]
    public async Task<ActionResult<MediaResponseDto>> PostMedia(MediaCreateDto mediaRequest)
    {
        try
        {
            var media = new Media
            {
                ProjectId = mediaRequest.ProjectId,
                ReportId = mediaRequest.ReportId,
                Name = mediaRequest.Name,
                Type = mediaRequest.Type,
                Path = mediaRequest.Path,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Media.Add(media);
            await _context.SaveChangesAsync();

            await _logService.CreateLog(new Log
            {
                entity_id = media.Id,
                entity_name = Entity,
                user_id = 1,
                action = LogAction.Create
            });

            return await GetMedia(media.Id);

        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"Error creating Media: {e.Message}");
            throw;
        }
    }
    
    // PUT: api/Media/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutMedia(int id, MediaUpdateDto mediaRequest)
    {
        var media = await _context.Media.FindAsync(id);

        if (media == null)
            return NotFound();
        try
        {
            media.ProjectId = mediaRequest.ProjectId ?? media.ProjectId;
            media.ReportId = mediaRequest.ReportId ?? media.ReportId;
            media.Name = mediaRequest.Name ?? media.Name;
            media.Type = mediaRequest.Type ?? media.Type;
            media.Path = mediaRequest.Path ?? media.Path;
            media.UpdatedAt = DateTime.UtcNow;
            
            await _logService.CreateLog(new Log
            {
                entity_id = media.Id,
                entity_name = Entity,
                user_id = 1,
                action = LogAction.Update
            });

            await _context.SaveChangesAsync();
            
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Media.Any(a => a.Id == id))
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
    
    // DELETE: api/Media/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMedia(int id)
    {
        try
        {
            var media = await _context.Media.FindAsync(id);

            if (media == null)
            {
                return NotFound();
            }

            media.DeletedAt = DateTime.UtcNow;
            media.UpdatedAt = DateTime.UtcNow;

            await _logService.CreateLog(new Log
            {
                entity_id = media.Id,
                entity_name = Entity,
                user_id = 1,
                action = LogAction.Delete
            });

            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"Error deleting Media: {e.Message}");
            throw;
        }
        return NoContent();
    }
    
}

    

