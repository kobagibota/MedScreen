using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BaseLibrary.Entities;

namespace ServerLibrary.Configurations
{
    public class StandardConfiguration : IEntityTypeConfiguration<Standard>
    {
        public void Configure(EntityTypeBuilder<Standard> builder)
        {
            builder.ToTable("Standards");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasIndex(x => x.StandardName);
            builder.Property(x => x.StandardName).IsRequired();
        }
    }
}
