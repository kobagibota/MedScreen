using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MQC.BaseLibrary.Entities;

namespace MQC.ServerLibrary.Configurations
{
    public class UseWithConfiguration : IEntityTypeConfiguration<UseWith>
    {
        public void Configure(EntityTypeBuilder<UseWith> builder)
        {
            builder.ToTable("UseWiths");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.QCId).IsRequired();
            builder.Property(x => x.SupplyId).IsRequired();
            builder.Property(x => x.LotSupplyId).IsRequired();

            builder.HasOne(o => o.QC).WithMany(m => m.UseWiths).HasForeignKey(i => i.QCId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(o => o.Supply).WithMany(m => m.UseWiths).HasForeignKey(i => i.SupplyId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.LotSupply).WithMany(m => m.UseWiths).HasForeignKey(i => i.LotSupplyId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
