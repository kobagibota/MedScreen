using Microsoft.AspNetCore.Identity;

namespace BaseLibrary.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        public int LabId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }
        public string? Avatar { get; set; }

        public virtual Laboratory Laboratory { get; set; }

        public List<QC>? QCs { get; set; }
        public List<AppLog>? AppLogs { get; set; }
        public List<Result>? Results { get; set; }
    }
}
