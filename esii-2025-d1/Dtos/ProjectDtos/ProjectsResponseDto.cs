namespace esii_2025_d1.Dtos.ProjectDtos
{
    public class ProjectsResponseDto
    {
        public int Id { get; set; }
<<<<<<< HEAD
        public int UserId { get; set; }
=======
        public string? UserId { get; set; }
>>>>>>> origin/develop
        public int CustomerId { get; set; }
        public string? Name { get; set; }
        public float? HourlyRate { get; set; }
        public int? DailyWorkHours { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
