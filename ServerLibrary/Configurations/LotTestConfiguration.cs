using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MQC.BaseLibrary.Entities;

namespace MQC.ServerLibrary.Configurations
{
    public class LotTestConfiguration : IEntityTypeConfiguration<LotTest>
    {
        public void Configure(EntityTypeBuilder<LotTest> builder)
        {
            builder.ToTable("LotTests");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.TestQCId).IsRequired();
            builder.Property(x => x.LotNumber).IsRequired();
            builder.Property(x => x.ExpDate).IsRequired().HasColumnType("date").HasAnnotation("CheckConstraint", "ExpDate >= GETDATE()");

            builder.HasOne(o => o.TestQC).WithMany(m => m.LotTests).HasForeignKey(i => i.TestQCId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
