using esii_2025_d1.Data;
using esii_2025_d1.Models;
using esii_2025_d1.Dtos.UsersDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace esii_2025_d1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public UserController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/User
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UsersResponseDto>>> GetUsers()
    {
        var users = await _context.Users
        .Where(u => u.DeletedAt == null)
            .Select(user => new UsersResponseDto
            {
                Id = user.Id,
                RoleId = user.RoleId,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                DailyWorkHours = user.DailyWorkHours,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            })
            .ToListAsync();

        return Ok(users);
    }

    // GET: api/User/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<UsersResponseDto>> GetUser(int id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null || user.DeletedAt != null)
        {
            return NotFound();
        }

        var userDto = new UsersResponseDto
        {
            Id = user.Id,
            RoleId = user.RoleId,
            Name = user.Name,
            Email = user.Email,
            Password = user.Password,
            DailyWorkHours = user.DailyWorkHours,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt
        };

        return Ok(userDto);
    }

    // POST: api/User
    [HttpPost]
    public async Task<ActionResult<UsersCreateDto>> PostUser(UsersCreateDto userDto)
    {
        var user = new User
        {
            RoleId = userDto.RoleId,
            Name = userDto.Name,
            Email = userDto.Email,
            Password = userDto.Password,
            DailyWorkHours = userDto.DailyWorkHours,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }

    // PUT: api/User/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(int id, UsersUpdateDto userDto)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null || user.DeletedAt != null)
        {
            return NotFound();
        }

        user.RoleId = userDto.RoleId; //?? user.RoleId;
        user.Name = userDto.Name ?? user.Name;
        user.Email = userDto.Email ?? user.Email;
        user.Password = userDto.Password ?? user.Password;
        user.DailyWorkHours = userDto.DailyWorkHours; //?? user.DailyWorkHours;
        user.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/User/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null || user.DeletedAt != null)
        {
            return NotFound();
        }

        user.DeletedAt = DateTime.UtcNow;
        user.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return NoContent();
    }
}
