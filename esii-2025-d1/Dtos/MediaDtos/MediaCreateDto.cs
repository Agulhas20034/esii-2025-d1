namespace esii_2025_d1.Dtos.MediaDtos;

public class MediaCreateDto
{
    public int ProjectId { get; set; } // Precisa de ter um projeto associado

    public int? ReportId { get; set; } // Precisa de um ProjectReport?? associado

    public string Name { get; set; }

    public string Type { get; set; }

    public string Path { get; set; }
}