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
    //Função pra obter todos os users
    public List<ApplicationUser> GetAllUsers() => _users.Values.ToList();
    //Função para criar novo user
    public async Task<(bool Success, string ErrorMessage)> CreateUserAsync(string email, string password, List<string> roles)
    {
        using var scope = _scopeFactory.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        //Valida se email ja esta registado
        if (await userManager.FindByEmailAsync(email) != null)
        {
            return (false, "Email already exists");
        }

        //Cria novo user
        var user = new ApplicationUser { UserName = email, Email = email };
        var createResult = await userManager.CreateAsync(user, password);

        if (!createResult.Succeeded)
        {
            return (false, string.Join(", ", createResult.Errors.Select(e => e.Description)));
        }

        //Adiciona Role
        foreach (var role in roles)
        {
            //Verifica se o role existe
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

        //Adiciona user criado a cache(singleton feature)
        _users.TryAdd(user.Id, user);

        return (true, "UserInfo created successfully");
    }
    //Busca user por id
    public async Task<ApplicationUser?> GetUserByIdAsync(string userId)
{
    // Tenta primeiro ir buscar á cache
    if (_users.TryGetValue(userId, out var cachedUser))
        return cachedUser;

    // Em caso de erro vai á db
    using var scope = _scopeFactory.CreateScope();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var user = await userManager.FindByIdAsync(userId);
    
    if (user != null)
    {
        _users.TryAdd(user.Id, user);
    }
    
    return user;
}
    //Busca user roles por id do role

    public async Task<List<string>> GetUserRolesAsync(string userId)
    {
        using var scope = _scopeFactory.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var user = await userManager.FindByIdAsync(userId);
        return user != null ? (await userManager.GetRolesAsync(user)).ToList() : new List<string>();
    }
//Atualiza UserInfo
    public async Task<bool> UpdateUserAsync(ApplicationUser user)
    {
        using var scope = _scopeFactory.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    
        // Pega no user da DB
        var dbUser = await userManager.FindByIdAsync(user.Id);
    
        // Atualiza dados
        dbUser.Email = user.Email;
        dbUser.UserName = user.Email;
        dbUser.EmailConfirmed = user.EmailConfirmed;
    
        // Grava
        var result = await userManager.UpdateAsync(dbUser);
    
        // Atualiza cache
        if (result.Succeeded)
        {
            _users.AddOrUpdate(user.Id, _ => 
                {
                    // Cria nova instancia pra evitar erros
                    return new ApplicationUser 
                    {
                        Id = dbUser.Id,
                        Email = dbUser.Email,
                        UserName = dbUser.UserName,
                        EmailConfirmed = dbUser.EmailConfirmed
                        
                    };
                }, 
                (_, existing) => 
                {
                    // Atualiza instancia existente
                    existing.Email = dbUser.Email;
                    existing.UserName = dbUser.UserName;
                    existing.EmailConfirmed = dbUser.EmailConfirmed;
                    return existing;
                });
        }
    
        return result.Succeeded;
    }
//Atualiza roles do user
    public async Task<bool> UpdateUserRoleAsync(string userId, string role)
    {
        using var scope = _scopeFactory.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        // Verifica se role existe
        if (!await roleManager.RoleExistsAsync(role))
            return false;

        var user = await userManager.FindByIdAsync(userId);
        if (user == null) return false;

        // Apaga role atual e adiciona a atualizada
        var currentRoles = await userManager.GetRolesAsync(user);
        await userManager.RemoveFromRolesAsync(user, currentRoles);
        
        // Adiciona nova role
        var result = await userManager.AddToRoleAsync(user, role);
        
        // Atualiza cache
        if (result.Succeeded && _users.TryGetValue(userId, out var cachedUser))
        {
            // Atualiza dados do user
            _users.TryRemove(userId, out _);
            _ = await GetUserByIdAsync(userId);
        }
        
        return result.Succeeded;
    }
}