using esii_2025_d1.Models.Enums;

namespace esii_2025_d1.Dtos.ProjectUserDtos
{
    public class ProjectUserUpdateDto
    {
        public int? ProjectId { get; set; }
        public string? UserId { get; set; }
        public string? InviterId { get; set; }
        public ProjectUserStatus? Status { get; set; }
    }
}
