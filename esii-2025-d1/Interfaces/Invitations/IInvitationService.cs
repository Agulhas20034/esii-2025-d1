using esii_2025_d1.ViewModels;

namespace esii_2025_d1.Interfaces.Invitations
{
    public interface IInvitationService
    {
        Task<List<Invitation>> GetPendingInvitations(string currentUserId);
    }

}
