using esii_2025_d1.Data;
using esii_2025_d1.Dtos.CustomersDtos;
using esii_2025_d1.Models;

namespace esii_2025_d1.Controllers;

using esii_2025_d1.Dtos.JomDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    
    public CustomerController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    // GET: api/Jom
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CustomersResponseDto>>> GetCustomers()
    {
        var customers = await _context.Customers
            .Select(customer => new CustomersResponseDto
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                CreatedAt = customer.CreatedAt,
                UpdatedAt = customer.UpdatedAt
            })
            .ToListAsync();

        return Ok(customers);
    }
    
    // GET: api/Jom/"id"
    [HttpGet("{id}")]
    public async Task<ActionResult<CustomersResponseDto>> GetCustomer(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
    
        if (customer == null)
        {
            return NotFound();
        }

        var customerResponse = new CustomersResponseDto
        {
            Id = customer.Id,
            Name = customer.Name,
            Email = customer.Email,
            PhoneNumber = customer.PhoneNumber,
            CreatedAt = customer.CreatedAt,
            UpdatedAt = customer.UpdatedAt
        };

        return Ok();
    }

    
    [HttpPost]
    public async Task<ActionResult<MediaCreateDto>> PostJom(MediaCreateDto jomRequest)
    {
        var jom = new Jom
        {
            Label = jomRequest.Label,
            Date = jomRequest.Date ?? DateTime.UtcNow,
            IsDone = jomRequest.IsDone,
            TestNumber = jomRequest.TestNumber,
            created_at = DateTime.UtcNow,
            updated_at = DateTime.UtcNow,
        };
        
        _context.Joms.Add(jom);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetJom), new { id = jom.Id }, jom);
    }
        
    // PUT: api/Jom/"id"
    [HttpPut("{id}")]
    public async Task<IActionResult> PutJom(int id, MediaUpdateDto jom)
    {
        var existingJom = await _context.Joms.FindAsync(id);
        
        if (existingJom == null)
        {
            return NotFound();
        }
        
        // Update only the modified properties
        existingJom.Label = jom.Label ?? existingJom.Label;
        existingJom.Date = jom.Date != default ? jom.Date : existingJom.Date;
        existingJom.IsDone = jom.IsDone;
        existingJom.TestNumber = jom.TestNumber ?? existingJom.TestNumber;
        existingJom.updated_at = DateTime.UtcNow;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Joms.Any(e => e.Id == id))
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
    
    // DELETE: api/Jom/"id"
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(int id)
    {
        var user = await _context.Joms.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        user.UpdatedAt = DateTime.UtcNow;
        user.DeletedAt = DateTime.UtcNow;
        
        await _context.SaveChangesAsync();
        return NoContent();
    }
}


