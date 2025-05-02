namespace esii_2025_d1.Dtos.MediaDtos;

public class MediaResponseDto
{
    public int Id { get; set; }

    public int ProjectId { get; set; }

    public int ReportId { get; set; }

    public string Name { get; set; }

    public string Type { get; set; }

    public string Path { get; set; }

    public DateTime Created_at { get; set; }

    public DateTime Updated_at { get; set; }

    public DateTime? Deleted_at { get; set; }
}