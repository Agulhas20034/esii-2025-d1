namespace esii_2025_d1.Dtos.ProjectUserDtos
{
    public class ProjectUsersCreateDto
    {
        public int ProjectId { get; set; }
        public string UserId { get; set; }
        public int InviterId { get; set; }
        public string Status { get; set; }
    }
}
