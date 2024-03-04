namespace BaseLibrary.Dtos
{
    public class LaboratoryDto
    {
        public required string OrganizationName { get; set; }
        public required string LabName { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public byte[]? Logo { get; set; }
    }
}
