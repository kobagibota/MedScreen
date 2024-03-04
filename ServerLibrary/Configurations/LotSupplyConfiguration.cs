using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BaseLibrary.Entities;

namespace ServerLibrary.Configurations
{
    public class LotSupplyConfiguration : IEntityTypeConfiguration<LotSupply>
    {
        public void Configure(EntityTypeBuilder<LotSupply> builder)
        {
            builder.ToTable("LotSupplies");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.SupplyId).IsRequired();
            builder.Property(x => x.LotNumber).IsRequired();
            builder.Property(x => x.ExpDate).IsRequired().HasColumnType("date").HasAnnotation("CheckConstraint", "ExpDate >= GETDATE()");

            builder.HasOne(o => o.Supply).WithMany(m => m.LotSupplies).HasForeignKey(i => i.SupplyId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
