using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MQC.BaseLibrary.Entities;

namespace MQC.ServerLibrary.Configurations
{
    public class SupplyProfileConfiguration : IEntityTypeConfiguration<SupplyProfile>
    {
        public void Configure(EntityTypeBuilder<SupplyProfile> builder)
        {
            builder.ToTable("SupplyProfiles");
            builder.HasKey(x => new { x.SupplyId, x.QCProfileId });

            builder.HasOne(o => o.Supply).WithMany(m => m.SupplyProfiles).HasForeignKey(i => i.SupplyId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.QCProfile).WithMany(m => m.SupplyProfiles).HasForeignKey(i => i.QCProfileId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
