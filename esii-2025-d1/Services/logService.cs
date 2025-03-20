using esii_2025_d1.Data;
using esii_2025_d1.Models;

namespace esii_2025_d1.Services;

public interface ILogService
{
    Task CreateLog(Log log);
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
            _context.logs.Add(log);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"Error creating log: {e.Message}");
        }
    }
}