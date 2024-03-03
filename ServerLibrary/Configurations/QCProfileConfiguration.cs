using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MQC.BaseLibrary.Entities;

namespace MQC.ServerLibrary.Configurations
{
    public class QCProfileConfiguration : IEntityTypeConfiguration<QCProfile>
    {
        public void Configure(EntityTypeBuilder<QCProfile> builder)
        {
            builder.ToTable("QCProfiles");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.LabId).IsRequired();
            builder.Property(x => x.MethodId).IsRequired();
            builder.Property(x => x.CategoryId).IsRequired();
            builder.Property(x => x.QCName).IsRequired().HasMaxLength(500);

            builder.HasOne(o => o.Laboratory).WithMany(m => m.QCProfiles).HasForeignKey(i => i.LabId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(o => o.Method).WithMany(m => m.QCProfiles).HasForeignKey(i => i.MethodId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(o => o.Category).WithMany(m => m.QCProfiles).HasForeignKey(i => i.CategoryId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
