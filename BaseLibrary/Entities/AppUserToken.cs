using Microsoft.AspNetCore.Identity;

namespace BaseLibrary.Entities
{
    public class AppUserToken : IdentityUserToken<Guid>
    {
        public DateTime ExpiresAt { get; set; }
        public bool Revoked { get; set; }

        public virtual AppUser User { get; set; }
    }
}