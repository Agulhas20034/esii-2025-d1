using esii_2025_d1.Dtos.CustomersDtos;
using esii_2025_d1.Models.Enums;

namespace esii_2025_d1.Dtos.ProjectUserDtos
{
    public class ProjectUserResponseDto
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        public int InviterId { get; set; }
        public ProjectUserStatus Status { get; set; }
        
    }
}
