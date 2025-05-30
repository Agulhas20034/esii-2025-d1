

using esii_2025_d1.Dtos.UserDtos;
using esii_2025_d1.Interfaces.Invitations;
using esii_2025_d1.Models;
using esii_2025_d1.ViewModels;

namespace esii_2025_d1.Services
{
    public class InvitationService : IInvitationService
    {
        private readonly HttpClient Http;

        public InvitationService(HttpClient http)
        {
            Http = http;
        }

        public async Task<List<Invitation>> GetPendingInvitations(string currentUserId)
        {
            var response = await Http.GetFromJsonAsync<List<ProjectUser>>("api/projectuser");
            var projects = await Http.GetFromJsonAsync<List<Project>>("api/project");
            var users = await Http.GetFromJsonAsync<List<UserFullResponseDto>>("api/user");

            List<Invitation> invitations = new();

            foreach (var r in response)
            {
                if (r.UserId == currentUserId && r.Status == Models.Enums.ProjectUserStatus.Pending && r.DeletedAt == null)
                {
                    var info = users.FirstOrDefault(u => u.UserInfo.UserId == currentUserId)?.UserInfo;
                    var invitation = new Invitation
                    {
                        invite = r,
                        userInfo = new UserInfo{
                            UserId = info.UserId,
                            Name = info.Name,
                            DailyWorkHours = info.DailyWorkHours,
                            CreatedAt = info.CreatedAt, 
                            UpdatedAt = info.UpdatedAt                            
                        },
                        project = projects.FirstOrDefault(p => p.Id == r.ProjectId)
                    };

                    invitations.Add(invitation);
                }
            }

            return invitations;
        }
    }

}
