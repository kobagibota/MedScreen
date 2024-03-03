using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MQC.BaseLibrary.Entities;

namespace MQC.ServerLibrary.Configurations
{
    public class StandardDetailConfiguration : IEntityTypeConfiguration<StandardDetail>
    {
        public void Configure(EntityTypeBuilder<StandardDetail> builder)
        {
            builder.ToTable("StandardDetails");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.CategoryId).IsRequired();
            builder.Property(x => x.MethodId).IsRequired();
            builder.Property(x => x.StandardId).IsRequired();
            builder.Property(x => x.TestQCId).IsRequired();
            builder.Property(x => x.StrainId).IsRequired();

            builder.HasOne(o => o.Category).WithMany(m => m.StandardDetails).HasForeignKey(i => i.CategoryId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(o => o.Method).WithMany(m => m.StandardDetails).HasForeignKey(i => i.MethodId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(o => o.Standard).WithMany(m => m.StandardDetails).HasForeignKey(i => i.StandardId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(o => o.TestQC).WithMany(m => m.StandardDetails).HasForeignKey(i => i.TestQCId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(o => o.Strain).WithMany(m => m.StandardDetails).HasForeignKey(i => i.StrainId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
