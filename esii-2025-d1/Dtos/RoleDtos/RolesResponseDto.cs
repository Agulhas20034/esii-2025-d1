namespace esii_2025_d1.Dtos.RoleDtos
{
    public class RolesResponseDto
    {
        public int Id { get; set; }
        public int PermissionId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
