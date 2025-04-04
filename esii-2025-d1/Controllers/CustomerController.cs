using esii_2025_d1.Data;
using esii_2025_d1.Dtos.CustomersDtos;
using esii_2025_d1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace esii_2025_d1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CustomerController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Customer
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CustomersResponseDto>>> GetCustomers()
    {
        var customers = await _context.Customers
            .Where(c => c.DeletedAt == null)
            .Select(customer => new CustomersResponseDto
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                CreatedAt = customer.CreatedAt,
                UpdatedAt = customer.UpdatedAt,
                DeletedAt = customer.DeletedAt
            })
            .ToListAsync();

        return Ok(customers);
    }

    // GET: api/Customer/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CustomersResponseDto>> GetCustomer(int id)
    {
        var customer = await _context.Customers.FindAsync(id);

        if (customer == null || customer.DeletedAt != null)
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
            UpdatedAt = customer.UpdatedAt,
            DeletedAt = customer.DeletedAt
        };

        return Ok(customerResponse);
    }

    // POST: api/Customer
    [HttpPost]
    public async Task<ActionResult<CustomersResponseDto>> PostCustomer(CustomersCreateDto dto)
    {
        var customer = new Customer
        {
            Name = dto.Name,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();

        var response = new CustomersResponseDto
        {
            Id = customer.Id,
            Name = customer.Name,
            Email = customer.Email,
            PhoneNumber = customer.PhoneNumber,
            CreatedAt = customer.CreatedAt,
            UpdatedAt = customer.UpdatedAt,
            DeletedAt = customer.DeletedAt
        };

        return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, response);
    }

    // PUT: api/Customer/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCustomer(int id, CustomersUpdateDto dto)
    {
        var existingCustomer = await _context.Customers.FindAsync(id);

        if (existingCustomer == null || existingCustomer.DeletedAt != null)
        {
            return NotFound();
        }

        existingCustomer.Name = dto.Name ?? existingCustomer.Name;
        existingCustomer.Email = dto.Email ?? existingCustomer.Email;
        existingCustomer.PhoneNumber = dto.PhoneNumber ?? existingCustomer.PhoneNumber;
        existingCustomer.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Customer/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer == null || customer.DeletedAt != null)
        {
            return NotFound();
        }

        customer.DeletedAt = DateTime.UtcNow;
        customer.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return NoContent();
    }
}
