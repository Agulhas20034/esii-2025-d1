using esii_2025_d1.Models.Enums;
using Microsoft.Build.Framework;

namespace esii_2025_d1.Dtos.ProjectUserDtos
{
    public class ProjectUserCreateDto
    {
        [Required]
        public int ProjectId { get; set; }
    
        [Required]
        public string UserId { get; set; }
    
        [Required]
        public string InviterId { get; set; }
    
        public ProjectUserStatus Status { get; set; } = ProjectUserStatus.Pending;
    }
}
