using esii_2025_d1.Data;
<<<<<<< HEAD
using esii_2025_d1.Models;
using esii_2025_d1.Dtos.UsersDtos;
=======
using esii_2025_d1.Dtos.UserDtos;
using esii_2025_d1.Dtos.UserInfoDtos;
using esii_2025_d1.Models;
using esii_2025_d1.Models.Enums;
using esii_2025_d1.Services;
using Microsoft.AspNetCore.Identity;
>>>>>>> origin/develop
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace esii_2025_d1.Controllers;

<<<<<<< HEAD
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public UserController(ApplicationDbContext context)
    {
        _context = context;
=======

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _context;
    private readonly ILogService _logService;
    protected string Entity = "User";

    public UserController(
        UserManager<ApplicationUser> userManager,
        ApplicationDbContext context,
        ILogService logService)
    {
        _userManager = userManager;
        _context = context;
        _logService = logService;
>>>>>>> origin/develop
    }

    // GET: api/User
    [HttpGet]
<<<<<<< HEAD
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
=======
    public async Task<ActionResult<IEnumerable<UserFullResponseDto>>> GetUsers()
    {
        try
        {
            var users = await _userManager.Users
                .Select(u => new UserFullResponseDto
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    UserInfo = _context.UserInfos
                        .Where(ui => ui.UserId == u.Id)
                        .Select(ui => new UserInfoResponseDto
                        {
                            UserId = ui.UserId,
                            Name = ui.Name,
                            DailyWorkHours = ui.DailyWorkHours,
                            CreatedAt = ui.CreatedAt,
                            UpdatedAt = ui.UpdatedAt
                        })
                        .FirstOrDefault()
                })
                .ToListAsync();

            await _logService.CreateLog(new Log
            {
                entity_id = null,
                entity_name = Entity,
                user_id = "1",
                action = LogAction.Read
            });

            return Ok(users);
        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"Error fetching users: {e.Message}");
            throw;
        }
>>>>>>> origin/develop
    }

    // GET: api/User/{id}
    [HttpGet("{id}")]
<<<<<<< HEAD
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
=======
    public async Task<ActionResult<UserFullResponseDto>> GetUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return NotFound();

        var userInfo = await _context.UserInfos
            .Where(ui => ui.UserId == id)
            .Select(ui => new UserInfoResponseDto
            {
                UserId = ui.UserId,
                Name = ui.Name,
                DailyWorkHours = ui.DailyWorkHours,
                CreatedAt = ui.CreatedAt,
                UpdatedAt = ui.UpdatedAt
            })
            .FirstOrDefaultAsync();

        var response = new UserFullResponseDto
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            UserInfo = userInfo
        };

        await _logService.CreateLog(new Log
        {
            entity_id = null,
            entity_name = Entity,
            user_id = "1",
            action = LogAction.Read
        });

        return Ok(response);
    }

    [HttpPost("info")]
    public async Task<ActionResult<UserInfoResponseDto>> CreateUserInfo(UserInfoCreateDto userInfoRequest)
    {

        var userinfo = new UserInfo
        {
            UserId = userInfoRequest.UserId,
            Name = userInfoRequest.Name,
            DailyWorkHours = userInfoRequest.DailyWorkHours
        };
        _context.UserInfos.Add(userinfo);
        await _context.SaveChangesAsync();

        await _logService.CreateLog(new Log
        {
            entity_id = 1,
            entity_name = Entity,
            user_id = userInfoRequest.UserId
        });

        return Ok(Response);
    }

    //POST: api/User
    [HttpPost]
    public async Task<ActionResult<UserFullResponseDto>> CreateUser(
        [FromBody] UserCreateDto userDto,
        [FromQuery] UserInfoCreateDto? userInfoDto = null)
    {
        var user = new ApplicationUser
        {
            UserName = userDto.UserName,
            Email = userDto.Email,
            PhoneNumber = userDto.PhoneNumber
        };

        var result = await _userManager.CreateAsync(user, userDto.Password);
        if (!result.Succeeded) return BadRequest(result.Errors);
        
        if (userInfoDto != null)
        {
            var userInfo = new UserInfo
            {
                Name = userInfoDto.Name,
                DailyWorkHours = userInfoDto.DailyWorkHours,
                UserId = user.Id
            };
            _context.UserInfos.Add(userInfo);
            await _context.SaveChangesAsync();
        }

        await _logService.CreateLog(new Log
        {
            entity_id = null,
            entity_name = Entity,
            user_id = "1",
            action = LogAction.Create
        });
        
        var createdUser = await _userManager.FindByIdAsync(user.Id);
        var createdUserInfo = await _context.UserInfos
            .FirstOrDefaultAsync(ui => ui.UserId == user.Id);

        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, new UserFullResponseDto
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            UserInfo = createdUserInfo != null ? new UserInfoResponseDto
            {
                UserId = createdUserInfo.UserId,
                Name = createdUserInfo.Name,
                DailyWorkHours = createdUserInfo.DailyWorkHours,
                CreatedAt = createdUserInfo.CreatedAt,
                UpdatedAt = createdUserInfo.UpdatedAt
            } : null
        });
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(
        string id,
        [FromBody] UserUpdateDto userDto,
        [FromQuery] UserInfoUpdateDto? userInfoDto = null)
    {
      
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return NotFound();

        if (!string.IsNullOrEmpty(userDto.UserName))
            user.UserName = userDto.UserName;
        
        if (!string.IsNullOrEmpty(userDto.Email))
            user.Email = userDto.Email;
        
        if (userDto.PhoneNumber != null)
            user.PhoneNumber = userDto.PhoneNumber;

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded) return BadRequest(result.Errors);

      
        if (!string.IsNullOrEmpty(userDto.NewPassword))
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            result = await _userManager.ResetPasswordAsync(user, token, userDto.NewPassword);
            if (!result.Succeeded) return BadRequest(result.Errors);
        }
        
        if (userInfoDto != null)
        {
            var userInfo = await _context.UserInfos.FirstOrDefaultAsync(ui => ui.UserId == id);
            
            if (userInfo == null)
            {
                
                userInfo = new UserInfo
                {
                    UserId = id,
                    Name = userInfoDto.Name ?? "Geral", 
                    DailyWorkHours = userInfoDto.DailyWorkHours ?? 0
                };
                _context.UserInfos.Add(userInfo);
            }
            else
            {
                
                if (userInfoDto.Name != null)
                    userInfo.Name = userInfoDto.Name;
                
                if (userInfoDto.DailyWorkHours.HasValue)
                    userInfo.DailyWorkHours = userInfoDto.DailyWorkHours.Value;
                
                userInfo.UpdatedAt = DateTime.UtcNow;
            }
            
            await _context.SaveChangesAsync();
        }

        await _logService.CreateLog(new Log
        {
            entity_id = null,
            entity_name = Entity,
            user_id = "1",
            action = LogAction.Update
        });
>>>>>>> origin/develop

        return NoContent();
    }

    // DELETE: api/User/{id}
    [HttpDelete("{id}")]
<<<<<<< HEAD
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
=======
    public async Task<IActionResult> DeleteUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return NotFound();

    
        var userInfo = await _context.UserInfos.FirstOrDefaultAsync(ui => ui.UserId == id);
        if (userInfo != null)
        {
            _context.UserInfos.Remove(userInfo);
            await _context.SaveChangesAsync();
        }
        
        var result = await _userManager.DeleteAsync(user);
        if (!result.Succeeded) return BadRequest(result.Errors);

        await _logService.CreateLog(new Log
        {
            entity_id = null,
            entity_name = Entity,
            user_id = "1",
            action = LogAction.Delete
        });
>>>>>>> origin/develop

        return NoContent();
    }
}
