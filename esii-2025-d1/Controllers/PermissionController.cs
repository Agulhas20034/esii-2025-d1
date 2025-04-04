using esii_2025_d1.Data;
using esii_2025_d1.Dtos.PermissionDtos;
using esii_2025_d1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace esii_2025_d1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PermissionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Permission
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PermissionResponseDto>>> GetPermissions()
        {
            var permissions = await _context.Permissions
                .Select(p => new PermissionResponseDto
                {
                    Id = p.Id,
                    Label = p.Label,
                    Created_at = p.Created_at,
                    Updated_at = p.Updated_at,
                    Deleted_at = p.Deleted_at
                })
                .ToListAsync();

            return Ok(permissions);
        }

        // GET: api/Permission/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PermissionResponseDto>> GetPermissionById(int id)
        {
            var permission = await _context.Permissions.FindAsync(id);

            if (permission == null)
            {
                return NotFound();
            }

            var permissionResponse = new PermissionResponseDto
            {
                Id = permission.Id,
                Label = permission.Label,
                Created_at = permission.Created_at,
                Updated_at = permission.Updated_at,
                Deleted_at = permission.Deleted_at
            };

            return Ok(permissionResponse);
        }

        // POST: api/Permission
        [HttpPost]
        public async Task<ActionResult<PermissionResponseDto>> PostPermission(PermissionCreateDto permissionCreateDto)
        {
            var permission = new Permission
            {
                Label = permissionCreateDto.Label,
                Created_at = DateTime.UtcNow,
                Updated_at = DateTime.UtcNow
            };

            _context.Permissions.Add(permission);
            await _context.SaveChangesAsync();

            var permissionResponse = new PermissionResponseDto
            {
                Id = permission.Id,
                Label = permission.Label,
                Created_at = permission.Created_at,
                Updated_at = permission.Updated_at,
                Deleted_at = permission.Deleted_at
            };

            return CreatedAtAction(nameof(GetPermissionById), new { id = permission.Id }, permissionResponse);
        }

        // PUT: api/Permission/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPermission(int id, PermissionUpdateDto permissionUpdateDto)
        {
            var permission = await _context.Permissions.FindAsync(id);

            if (permission == null)
            {
                return NotFound();
            }

            permission.Label = permissionUpdateDto.Label ?? permission.Label;
            permission.Updated_at = DateTime.UtcNow;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Permissions.Any(p => p.Id == id))
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

        // DELETE: api/Permission/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePermission(int id)
        {
            var permission = await _context.Permissions.FindAsync(id);

            if (permission == null)
            {
                return NotFound();
            }

            permission.Deleted_at = DateTime.UtcNow;
            permission.Updated_at = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
