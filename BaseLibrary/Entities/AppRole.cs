using Microsoft.AspNetCore.Identity;

namespace BaseLibrary.Entities
{
    public class AppRole : IdentityRole<Guid>
    {
        public required string Description { get; set; }
    }
}
