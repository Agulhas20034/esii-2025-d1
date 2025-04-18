using esii_2025_d1.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;

namespace esii_2025_d1.Services;

public sealed class SingletonUserManager
{
    private static readonly Lazy<SingletonUserManager> _instance = 
        new(() => new SingletonUserManager());
    
    private readonly ConcurrentDictionary<string, ApplicationUser> _users = new();
    private IServiceScopeFactory _scopeFactory;

    private SingletonUserManager() { }

    public static SingletonUserManager Instance => _instance.Value;

    public void Initialize(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
        LoadUsersFromDatabase();
    }

    private void LoadUsersFromDatabase()
    {
        using var scope = _scopeFactory.CreateScope();
        var identityManager = scope.ServiceProvider
            .GetRequiredService<UserManager<ApplicationUser>>();
        
        foreach (var user in identityManager.Users.AsNoTracking())
        {
            _users.TryAdd(user.Id, user);
        }
    }

    public async Task<bool> DeleteUserAsync(string userId)
    {
        using var scope = _scopeFactory.CreateScope();
        var identityManager = scope.ServiceProvider
            .GetRequiredService<UserManager<ApplicationUser>>();

        var user = await identityManager.FindByIdAsync(userId);
        if (user == null) return false;

        var result = await identityManager.DeleteAsync(user);
        if (result.Succeeded)
        {
            _users.TryRemove(userId, out _);
            return true;
        }
        return false;
    }

    public List<ApplicationUser> GetAllUsers() => _users.Values.ToList();
    
    public async Task<(bool Success, string ErrorMessage)> CreateUserAsync(string email, string password, List<string> roles)
    {
        using var scope = _scopeFactory.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        // Validate email doesn't exist
        if (await userManager.FindByEmailAsync(email) != null)
        {
            return (false, "Email already exists");
        }

        // Create new user
        var user = new ApplicationUser { UserName = email, Email = email };
        var createResult = await userManager.CreateAsync(user, password);

        if (!createResult.Succeeded)
        {
            return (false, string.Join(", ", createResult.Errors.Select(e => e.Description)));
        }

        // Add roles
        foreach (var role in roles)
        {
            // Verify role exists
            if (!await roleManager.RoleExistsAsync(role))
            {
                await userManager.DeleteAsync(user);
                return (false, $"Role '{role}' does not exist");
            }

            var addToRoleResult = await userManager.AddToRoleAsync(user, role);
            if (!addToRoleResult.Succeeded)
            {
                await userManager.DeleteAsync(user);
                return (false, string.Join(", ", addToRoleResult.Errors.Select(e => e.Description)));
            }
        }

        // Add to in-memory cache
        _users.TryAdd(user.Id, user);

        return (true, "User created successfully");
    }
    public async Task<ApplicationUser?> GetUserByIdAsync(string userId)
{
    // Try to get from cache first
    if (_users.TryGetValue(userId, out var cachedUser))
        return cachedUser;

    // Fall back to database
    using var scope = _scopeFactory.CreateScope();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var user = await userManager.FindByIdAsync(userId);
    
    if (user != null)
    {
        _users.TryAdd(user.Id, user);
    }
    
    return user;
}

    public async Task<List<string>> GetUserRolesAsync(string userId)
    {
        using var scope = _scopeFactory.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var user = await userManager.FindByIdAsync(userId);
        return user != null ? (await userManager.GetRolesAsync(user)).ToList() : new List<string>();
    }

    public async Task<bool> UpdateUserAsync(ApplicationUser user)
    {
        using var scope = _scopeFactory.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        
        // Update in database
        var result = await userManager.UpdateAsync(user);
        
        // Update in cache if successful
        if (result.Succeeded)
        {
            _users.AddOrUpdate(user.Id, user, (id, existing) => user);
        }
        
        return result.Succeeded;
    }

    public async Task<bool> UpdateUserRoleAsync(string userId, string role)
    {
        using var scope = _scopeFactory.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        // Verify role exists
        if (!await roleManager.RoleExistsAsync(role))
            return false;

        var user = await userManager.FindByIdAsync(userId);
        if (user == null) return false;

        // Remove all existing roles
        var currentRoles = await userManager.GetRolesAsync(user);
        await userManager.RemoveFromRolesAsync(user, currentRoles);
        
        // Add new single role
        var result = await userManager.AddToRoleAsync(user, role);
        
        // Update cache
        if (result.Succeeded && _users.TryGetValue(userId, out var cachedUser))
        {
            // Trigger reload of this user's data
            _users.TryRemove(userId, out _);
            _ = await GetUserByIdAsync(userId);
        }
        
        return result.Succeeded;
    }
}