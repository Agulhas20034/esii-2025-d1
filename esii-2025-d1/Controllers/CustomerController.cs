using esii_2025_d1.Dtos.CustomersDtos;

namespace esii_2025_d1.Controllers;
using esii_2025_d1.Data;
using esii_2025_d1.Dtos.CustomersDtos;
using esii_2025_d1.Models;
using esii_2025_d1.Models.Enums;
using esii_2025_d1.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogService _logService;
    protected string Entity = "Customer";
    private readonly SingletonUserManager _usermanager;
    
    public CustomerController(ApplicationDbContext context, ILogService logService,SingletonUserManager usermanager)
    {
        _context = context;
        _logService = logService;
        _usermanager = usermanager;
    }
    
    // GET: api/Customer
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CustomerResponseDto>>> GetCustomers()
    {
        string? userId = await _usermanager.GetCurrentUserIdAsync();
        if (userId is null)
        {
            userId = "1";
        }
        try
        {
            var customers = await _context.Customers
                .AsNoTracking()
                .Select(customer => new CustomerResponseDto
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    PhoneNumber = customer.PhoneNumber,
                    Email = customer.Email,
                    Projects = customer.Projects.Select(p => new ProjectSimpleDto()
                    {
                        Id = p.Id,
                        UserId = p.UserId,
                        Name = p.Name,
                        Status = p.Status,
                    }).ToList(),
                    
                })
                .ToListAsync();
            
            await _logService.CreateLog(new Log
            {
                entity_id = null,
                entity_name = Entity,
                user_id = userId,
                action = LogAction.Read
            });

            return Ok(customers);
        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"Error fetching Customers: {e.Message}");
            throw;
        }
    }
    
    // GET: api/Customer/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerResponseDto>> GetCustomer(int id)
    {
        string? userId = await _usermanager.GetCurrentUserIdAsync();
        if (userId is null)
        {
            userId = "1";
        }
        // todo: se for necessario mais dados do projeto, criar um DTO
        
        var customer = await _context.Customers
            .Include(r => r.Projects)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (customer == null)
        {
            return NotFound();
        }

        try
        {
            var customerResponse = new CustomerResponseDto
            {
                Id = customer.Id,
                Name = customer.Name,
            Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                Email = customer.Email,
                Projects = customer.Projects.Select(p => new ProjectSimpleDto()
                {
                    Id = p.Id,
                    UserId = p.UserId,
                    Name = p.Name,
                    Status = p.Status,
                }).ToList(),
            };
            
            await _logService.CreateLog(new Log
            {
                entity_id = customer.Id,
                entity_name = Entity,
                user_id = userId,
                action = LogAction.Read
            });

            return Ok(customerResponse);
        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"Error fetching Customer: {e.Message}");
            throw;
        }
    }
    
    // POST: api/Customer
    [HttpPost]
    public async Task<ActionResult<CustomerResponseDto>> PostCustomer(CustomerCreateDto customerRequest)
    {
        string? userId = await _usermanager.GetCurrentUserIdAsync();
        if (userId is null)
        {
            userId = "1";
        }
        try
        {
            var customer = new Customer
            {
                Name = customerRequest.Name,
                Email = customerRequest.Email,
                PhoneNumber = customerRequest.PhoneNumber,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            
            // Adiciona Project (se existirem IDs)
            if (customerRequest.ProjectIds != null && customerRequest.ProjectIds.Any())
            {
                var project = await _context.Projects
                    .Where(m => customerRequest.ProjectIds.Contains(m.Id))
                    .ToListAsync();

                customer.Projects = project;
            }
            
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            await _logService.CreateLog(new Log
            {
                entity_id = customer.Id,
                entity_name = Entity,
                user_id = userId,
                action = LogAction.Create
            });

            return await GetCustomer(customer.Id);

        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"Error creating Customer: {e.Message}");
            throw;
        }
    }
    
    // PUT: api/Customer/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCustomer(int id, CustomerUpdateDto customerRequest)
    {
        string? userId = await _usermanager.GetCurrentUserIdAsync();
        if (userId is null)
        {
            userId = "1";
        }
        var customer = await _context.Customers
            .Include(p => p.Projects) 
            .FirstOrDefaultAsync(c => c.Id == id);

        if (customer == null)
            return NotFound();
        try
        {
            customer.Name = customerRequest.Name ?? customer.Name;
            customer.Email = customerRequest.Email ?? customer.Email;
            customer.PhoneNumber = customerRequest.PhoneNumber ?? customer.PhoneNumber;
            
            if (customerRequest.ProjectIds != null && customerRequest.ProjectIds.Any())
            {
                var newProject = await _context.Projects
                    .Where(m => customerRequest.ProjectIds.Contains(m.Id))
                    .ToListAsync();
                
                if (newProject.Count != customerRequest.ProjectIds.Count)
                {
                    var missingIds = customerRequest.ProjectIds.Except(newProject.Select(m => m.Id));
                    return BadRequest($"the project id does not exist: {string.Join(", ", missingIds)}");
                }
                
                customer.Projects = newProject;
            }
            customer.UpdatedAt = DateTime.UtcNow;
            
            await _logService.CreateLog(new Log
            {
                entity_id = customer.Id,
                entity_name = Entity,
                user_id = userId,
                action = LogAction.Update
            });

            await _context.SaveChangesAsync();
            
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Customers.Any(a => a.Id == id))
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
    
    // DELETE: api/Customer/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(int id)
    {
        string? userId = await _usermanager.GetCurrentUserIdAsync();
        if (userId is null)
        {
            userId = "1";
        }
        try
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            customer.DeletedAt = DateTime.UtcNow;
            customer.UpdatedAt = DateTime.UtcNow;

            await _logService.CreateLog(new Log
            {
                entity_id = customer.Id,
                entity_name = Entity,
                user_id = userId,
                action = LogAction.Delete
            });

            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"Error deleting Customer: {e.Message}");
            throw;
        }
        return NoContent();
    }
}


    
    
    
    
    
    
    


    

