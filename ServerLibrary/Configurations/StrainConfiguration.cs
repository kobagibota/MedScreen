using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MQC.BaseLibrary.Entities;

namespace MQC.ServerLibrary.Configurations
{
    public class StrainConfiguration : IEntityTypeConfiguration<Strain>
    {
        public void Configure(EntityTypeBuilder<Strain> builder)
        {
            builder.ToTable("Strains");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.GroupId).IsRequired();
            builder.Property(x => x.StrainName).IsRequired();

            builder.HasOne(o => o.StrainGroup).WithMany(m => m.Strains).HasForeignKey(i => i.GroupId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
