namespace esii_2025_d1.Dtos.MediaDtos;

public class MediaUpdateDto
{
    public int? ProjectId { get; set; }

    public int? ReportId { get; set; }

    public string? Name { get; set; }

    public string? Type { get; set; }

    public string? Path { get; set; }
}