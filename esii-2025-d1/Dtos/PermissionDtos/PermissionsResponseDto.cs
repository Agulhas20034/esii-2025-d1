namespace esii_2025_d1.Dtos.PermissionDtos
{
    public class PermissionResponseDto
    {
        public int Id { get; set; }

        public string Label { get; set; }

        public DateTime Created_at { get; set; }

        public DateTime Updated_at { get; set; }

        public DateTime? Deleted_at { get; set; }
    }
}