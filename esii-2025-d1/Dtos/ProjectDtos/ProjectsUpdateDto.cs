namespace esii_2025_d1.Dtos.ProjectDtos
{
    public class ProjectsUpdateDto
    {
        public string? UserId { get; set; }
        public int? CustomerId { get; set; }
        public string? Name { get; set; }
        public float? HourlyRate { get; set; }
        public int? DailyWorkHours { get; set; }
    }
}