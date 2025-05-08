// Services/ProjectNotificationService.cs
using esii_2025_d1.Data;
using esii_2025_d1.Models;
using Microsoft.EntityFrameworkCore;

namespace esii_2025_d1.Services
{
    public interface IProjectNotificationService
    {
        Task NotifyProjectUpdated(int projectId);
    }

    public class ProjectNotificationService : IProjectNotificationService
    {
        private readonly ApplicationDbContext _context;

        public ProjectNotificationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task NotifyProjectUpdated(int projectId)
        {
            var project = await _context.Projects.FindAsync(projectId);
            if (project != null)
            {
                project.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }
    }
}