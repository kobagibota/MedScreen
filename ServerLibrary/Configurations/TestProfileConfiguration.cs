using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BaseLibrary.Entities;

namespace ServerLibrary.Configurations
{
    public class TestProfileConfiguration : IEntityTypeConfiguration<TestProfile>
    {
        public void Configure(EntityTypeBuilder<TestProfile> builder)
        {
            builder.ToTable("TestProfiles");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.CategoryId).IsRequired();
            builder.Property(x => x.MethodId).IsRequired();
            builder.Property(x => x.TestTypeId).IsRequired();

            builder.HasOne(o => o.TestType).WithMany(m => m.TestProfiles).HasForeignKey(i => i.TestTypeId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(o => o.Method).WithMany(m => m.TestProfiles).HasForeignKey(i => i.MethodId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(o => o.Category).WithMany(m => m.TestProfiles).HasForeignKey(i => i.CategoryId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
