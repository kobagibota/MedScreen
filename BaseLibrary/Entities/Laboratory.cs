using BaseLibrary.Extentions;

namespace BaseLibrary.Entities
{
    public class Laboratory
    {
        public int Id { get; set; }
        public required string OrganizationName { get; set; }
        public required string LabName { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public byte[]? Logo { get; set; }
        public LabStatus LabStatus { get; set; }

        public List<AppUser>? Users { get; set; }
        public List<QC>? QCs { get; set; }
        public List<QCProfile>? QCProfiles { get; set; }
    }
}
