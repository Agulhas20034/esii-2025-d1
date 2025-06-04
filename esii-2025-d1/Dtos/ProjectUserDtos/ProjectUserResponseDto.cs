using esii_2025_d1.Dtos.CustomersDtos;
using esii_2025_d1.Models.Enums;

namespace esii_2025_d1.Dtos.ProjectUserDtos
{
    public class ProjectUserResponseDto
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string UserId { get; set; }
        public string InviterId { get; set; }
        public ProjectUserStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
