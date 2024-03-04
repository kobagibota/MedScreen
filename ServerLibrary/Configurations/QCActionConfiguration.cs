using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BaseLibrary.Entities;

namespace ServerLibrary.Configurations
{
    public class QCActionConfiguration : IEntityTypeConfiguration<QCAction>
    {
        public void Configure(EntityTypeBuilder<QCAction> builder)
        {
            builder.ToTable("QCActions");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasIndex(x => x.ActionName);
            builder.Property(x => x.ActionName).IsRequired();
        }
    }
}
