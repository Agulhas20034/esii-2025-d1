using esii_2025_d1.Dtos.MediaDtos;
using esii_2025_d1.Models;

namespace esii_2025_d1.Dtos.ReportDtos
{
    public class ReportResponseDto
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? ProjectId { get; set; }
        
        public virtual ICollection<MediaResponseDto> Media { get; set; } = new List<MediaResponseDto>();
    }
}
