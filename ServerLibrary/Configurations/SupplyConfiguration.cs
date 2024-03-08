using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BaseLibrary.Entities;

namespace ServerLibrary.Configurations
{
    public class SupplyConfiguration : IEntityTypeConfiguration<Supply>
    {
        public void Configure(EntityTypeBuilder<Supply> builder)
        {
            builder.ToTable("Supplies");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasIndex(x => x.SupplyName).IsUnique();

            builder.Property(x => x.MethodId).IsRequired();
            builder.Property(x => x.SupplyName).IsRequired();

            builder.HasOne(o => o.Method).WithMany(m => m.Supplies).HasForeignKey(i => i.MethodId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
