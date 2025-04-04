using esii_2025_d1.Data;
using esii_2025_d1.Dtos.MediaDtos;
using esii_2025_d1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace esii_2025_d1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MediaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Media
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MediaResponseDto>>> GetMedia()
        {
            var media = await _context.Media
                .Select(m => new MediaResponseDto
                {
                    Id = m.Id,
                    ProjectId = m.ProjectId,
                    ReportId = m.ReportId,
                    Name = m.Name,
                    Type = m.Type,
                    Path = m.Path,
                    Created_at = m.Created_at,
                    Updated_at = m.Updated_at,
                    Deleted_at = m.Deleted_at
                })
                .ToListAsync();

            return Ok(media);
        }

        // GET: api/Media/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<MediaResponseDto>> GetMediaById(int id)
        {
            var media = await _context.Media.FindAsync(id);

            if (media == null)
            {
                return NotFound();
            }

            var mediaResponse = new MediaResponseDto
            {
                Id = media.Id,
                ProjectId = media.ProjectId,
                ReportId = media.ReportId,
                Name = media.Name,
                Type = media.Type,
                Path = media.Path,
                Created_at = media.Created_at,
                Updated_at = media.Updated_at,
                Deleted_at = media.Deleted_at
            };

            return Ok(mediaResponse);
        }

        // POST: api/Media
        [HttpPost]
        public async Task<ActionResult<MediaResponseDto>> PostMedia(MediaCreateDto mediaCreateDto)
        {
            var media = new Media
            {
                ProjectId = mediaCreateDto.ProjectId,
                ReportId = mediaCreateDto.ReportId,
                Name = mediaCreateDto.Name,
                Type = mediaCreateDto.Type,
                Path = mediaCreateDto.Path,
                Created_at = DateTime.UtcNow,
                Updated_at = DateTime.UtcNow
            };

            _context.Media.Add(media);
            await _context.SaveChangesAsync();

            var mediaResponse = new MediaResponseDto
            {
                Id = media.Id,
                ProjectId = media.ProjectId,
                ReportId = media.ReportId,
                Name = media.Name,
                Type = media.Type,
                Path = media.Path,
                Created_at = media.Created_at,
                Updated_at = media.Updated_at,
                Deleted_at = media.Deleted_at
            };

            return CreatedAtAction(nameof(GetMediaById), new { id = media.Id }, mediaResponse);
        }

        // PUT: api/Media/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedia(int id, MediaUpdateDto mediaUpdateDto)
        {
            var media = await _context.Media.FindAsync(id);

            if (media == null)
            {
                return NotFound();
            }

            media.ProjectId = mediaUpdateDto.ProjectId ?? media.ProjectId;
            media.ReportId = mediaUpdateDto.ReportId ?? media.ReportId;
            media.Name = mediaUpdateDto.Name ?? media.Name;
            media.Type = mediaUpdateDto.Type ?? media.Type;
            media.Path = mediaUpdateDto.Path ?? media.Path;
            media.Updated_at = DateTime.UtcNow;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Media.Any(m => m.Id == id))
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
            var media = await _context.Media.FindAsync(id);

            if (media == null)
            {
                return NotFound();
            }

            media.Deleted_at = DateTime.UtcNow;
            media.Updated_at = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
