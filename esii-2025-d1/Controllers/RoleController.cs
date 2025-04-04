using esii_2025_d1.Data;
using esii_2025_d1.Models;
using esii_2025_d1.Dtos.RoleDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace esii_2025_d1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RoleController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Role
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RolesResponseDto>>> GetRoles()
        {
            var roles = await _context.Roles
                .Select(role => new RolesResponseDto
                {
                    Id = role.Id,
                    PermissionId = role.PermissionId,
                    Name = role.Name,
                    CreatedAt = role.CreatedAt,
                    UpdatedAt = role.UpdatedAt,
                    DeletedAt = role.DeletedAt
                })
                .ToListAsync();

            return Ok(roles);
        }

        // GET: api/Role/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<RolesResponseDto>> GetRole(int id)
        {
            var role = await _context.Roles.FindAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            var roleResponse = new RolesResponseDto
            {
                Id = role.Id,
                PermissionId = role.PermissionId,
                Name = role.Name,
                CreatedAt = role.CreatedAt,
                UpdatedAt = role.UpdatedAt,
                DeletedAt = role.DeletedAt
            };

            return Ok(roleResponse);
        }

        // POST: api/Role
        [HttpPost]
        public async Task<ActionResult<RolesResponseDto>> PostRole(RolesCreateDto roleCreateDto)
        {
            var role = new Role
            {
                PermissionId = roleCreateDto.PermissionId,
                Name = roleCreateDto.Name,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            _context.Roles.Add(role);
            await _context.SaveChangesAsync();

            var roleResponse = new RolesResponseDto
            {
                Id = role.Id,
                PermissionId = role.PermissionId,
                Name = role.Name,
                CreatedAt = role.CreatedAt,
                UpdatedAt = role.UpdatedAt,
                DeletedAt = role.DeletedAt
            };

            return CreatedAtAction(nameof(GetRole), new { id = role.Id }, roleResponse);
        }

        /*
        // PUT: api/Role/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole(int id, RolesUpdateDto rolesUpdateDto)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null)
            {
                return NotFound();
            }
        } */
        // Em construção
        // 

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
