using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BaseLibrary.Entities;

namespace ServerLibrary.Configurations
{
    public class TestQCConfiguration : IEntityTypeConfiguration<TestQC>
    {
        public void Configure(EntityTypeBuilder<TestQC> builder)
        {
            builder.ToTable("TestQCs");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasIndex(x => x.TestQCName).IsUnique();

            builder.Property(x => x.TestTypeId).IsRequired();
            builder.Property(x => x.TestQCName).IsRequired();

            builder.HasOne(o => o.TestType).WithMany(m => m.TestQCs).HasForeignKey(i => i.TestTypeId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
