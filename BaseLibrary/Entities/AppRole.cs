using Microsoft.AspNetCore.Identity;

namespace MQC.BaseLibrary.Entities
{
    public class AppRole : IdentityRole<Guid>
    {
        public required string Description { get; set; }
    }
}
