using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MQC.BaseLibrary.Entities;

namespace MQC.ServerLibrary.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable("AppUsers");
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.UserName);
            builder.Property(x => x.UserName).IsRequired().IsUnicode(false).HasMaxLength(200);
            builder.Property(x => x.FirstName).HasMaxLength(100);
            builder.Property(x => x.LastName).HasMaxLength(200);
            builder.Property(x => x.LabId).IsRequired();

            builder.HasOne(o => o.Laboratory).WithMany(m => m.Users).HasForeignKey(i => i.LabId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
