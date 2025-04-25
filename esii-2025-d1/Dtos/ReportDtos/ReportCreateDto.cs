namespace esii_2025_d1.Dtos.ReportDtos
{
    public class ReportCreateDto
    {
        public int UserId { get; set; }
        public int ProjectId { get; set; }

        public List<int>? MediaIds { get; set; } = new List<int>();
    }
}
