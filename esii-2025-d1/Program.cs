using esii_2025_d1.Components;
using esii_2025_d1.Data;
using esii_2025_d1.Services;
using esii_2025_d1.Components.Account;
using Microsoft.AspNetCore.Antiforgery; // tr
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Components.Authorization; // tr
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Mvc; // tr


var builder = WebApplication.CreateBuilder(args);

// Explicitly set Kestrel to listen on both HTTP and HTTPS
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5049);  // HTTP
    options.ListenAnyIP(7185, listenOptions =>
    {
        listenOptions.UseHttps(); // HTTPS
    });
});


// Adicionar BlazorBootstrap
builder.Services.AddBlazorBootstrap();
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Adicionar controllers de API
builder.Services.AddControllers();

// Adicionar serviços
builder.Services.AddScoped<ILogService, LogService>();

// Adicionar HttpClient
builder.Services.AddHttpClient();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7185") });

// Adicionar serviços do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Sandbox API",
        Version = "v1",
        Description = "API para a aplicação Blazor Sandbox"
    });
});

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
})
    .AddIdentityCookies();


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));
//builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

// Regista serviço singleton para uso
builder.Services.AddSingleton<SingletonUserManager>(provider => 
{
    var manager = SingletonUserManager.Instance;
    manager.Initialize(provider.GetRequiredService<IServiceScopeFactory>());
    return manager;
});

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();
builder.Services.AddHttpClient();

builder.Services.AddAuthorizationCore();
builder.Services.AddAntiforgery(options => {
    options.HeaderName = "X-CSRF-TOKEN";
    options.Cookie.Name = "__Host-CSRF";
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();

builder.Services.AddHttpContextAccessor();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sandbox API V1");
    });
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.MapControllers();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapPost("/Account/Logout", async (
    HttpContext context,
    [FromServices] SignInManager<ApplicationUser> signInManager) =>
{
    // Skip anti-forgery validation for logout
    await signInManager.SignOutAsync();
    return Results.LocalRedirect("~/");
}).DisableAntiforgery();

// tr .net authentication and authorization
app.UseAuthentication();
app.UseAuthorization();


app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints(); // tr



// Certifica-se que base de dados esta criada e conta admin/seeds estao seeded, tambem inicializa serviços registados
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var identityManager = app.Services.GetRequiredService<SingletonUserManager>();
    identityManager.Initialize(app.Services.GetRequiredService<IServiceScopeFactory>());
    await SeedRolesAndAdmin(roleManager, userManager);
}


app.Run();

// ====================================
// Roles seeded e Conta admin
// ====================================
async Task SeedRolesAndAdmin(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
{
    string[] roleNames = { "Admin", "UserManager", "User" };
    
    foreach (var role in roleNames)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    // Cria admin default se nao existir
    string adminEmail = "admin@example.com";
    string adminPassword = "Aa1234_"; 

    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        var newAdmin = new ApplicationUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
        var createUserResult = await userManager.CreateAsync(newAdmin, adminPassword);

        if (createUserResult.Succeeded)
        {
            await userManager.AddToRoleAsync(newAdmin, "Admin");
        }
    }
}