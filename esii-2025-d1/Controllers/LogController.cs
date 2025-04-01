using esii_2025_d1.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using esii_2025_d1.Models;
using Microsoft.EntityFrameworkCore;

namespace esii_2025_d1.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    public class LogController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LogController(ApplicationDbContext context)
        {
            _context = context;
        }
    
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Log>>> GetLogs()
        {
            return await _context.Logs.ToListAsync();
        }
        
    }
}

