using esii_2025_d1.Models;

namespace esii_2025_d1.Interfaces.ObserverPattern;

public interface IProjectObserver
{
    Task OnAssignmentChanged(int projectId);
    
}
