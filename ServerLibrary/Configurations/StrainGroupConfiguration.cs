using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MQC.BaseLibrary.Entities;

namespace MQC.ServerLibrary.Configurations
{
    public class StrainGroupConfiguration : IEntityTypeConfiguration<StrainGroup>
    {
        public void Configure(EntityTypeBuilder<StrainGroup> builder)
        {
            builder.ToTable("StrainGroups");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasIndex(x => x.GroupName);
            builder.Property(x => x.GroupName).IsRequired();
        }
    }
}
