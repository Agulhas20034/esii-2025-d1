// Services/LogService.cs
using esii_2025_d1.Data;
using esii_2025_d1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace esii_2025_d1.Services
{
    public interface ILogService
    {
        Task CreateLog(Log log);
        Task<List<Log>> GetRecentLogsAsync(int count = 100);
    }

    public class LogService : ILogService
    {
        private readonly ApplicationDbContext _context;

        public LogService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateLog(Log log)
        {
            try
            {
                // Ensure created_at is set (though your model already defaults to UtcNow)
                log.created_at = DateTime.UtcNow;
                _context.logs.Add(log);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"Error creating log: {e.Message}");
            }
        }

        public async Task<List<Log>> GetRecentLogsAsync(int count = 100)
        {
            return await _context.logs
                .OrderByDescending(l => l.created_at)  // Using created_at instead of Timestamp
                .Take(count)
                .ToListAsync();
        }
    }
}