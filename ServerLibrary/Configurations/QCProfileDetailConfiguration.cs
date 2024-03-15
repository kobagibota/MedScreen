using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BaseLibrary.Entities;

namespace ServerLibrary.Configurations
{
    public class QCProfileDetailConfiguration : IEntityTypeConfiguration<QCProfileDetail>
    {
        public void Configure(EntityTypeBuilder<QCProfileDetail> builder)
        {
            builder.ToTable("QCProfileDetails");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.QCProfileId).IsRequired();
            builder.Property(x => x.StandardDetailId).IsRequired();

            builder.HasOne(o => o.QCProfile).WithMany(m => m.QCProfileDetails).HasForeignKey(i => i.QCProfileId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(o => o.StandardDetail).WithMany(m => m.QCProfileDetails).HasForeignKey(i => i.StandardDetailId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
