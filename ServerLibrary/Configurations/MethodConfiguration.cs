using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BaseLibrary.Entities;

namespace ServerLibrary.Configurations
{
    public class MethodConfiguration : IEntityTypeConfiguration<Method>
    {
        public void Configure(EntityTypeBuilder<Method> builder)
        {
            builder.ToTable("Methods");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasIndex(x=>x.MethodName).IsUnique();
            builder.Property(x => x.MethodName).IsRequired();
        }
    }
}
