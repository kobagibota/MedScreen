using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BaseLibrary.Entities;

namespace ServerLibrary.Configurations
{
    public class AppUserTokenConfiguration : IEntityTypeConfiguration<AppUserToken>
    {
        public void Configure(EntityTypeBuilder<AppUserToken> builder)
        {
            builder.ToTable("AppUserTokens");

            builder.HasOne(o => o.User).WithMany(m => m.UserTokens).HasForeignKey(i => i.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}