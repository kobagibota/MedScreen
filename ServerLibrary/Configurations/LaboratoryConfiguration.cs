using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BaseLibrary.Entities;
using BaseLibrary.Extentions;

namespace ServerLibrary.Configurations
{
    public class LaboratoryConfiguration : IEntityTypeConfiguration<Laboratory>
    {
        public void Configure(EntityTypeBuilder<Laboratory> builder)
        {
            builder.ToTable("Laboratories");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasIndex(x => x.OrganizationName).IsUnique();
            builder.Property(x => x.LabName).IsRequired();
            builder.Property(x => x.LabStatus).HasDefaultValue(LabStatus.Active);
        }
    }
}
