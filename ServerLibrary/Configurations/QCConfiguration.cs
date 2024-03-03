using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MQC.BaseLibrary.Entities;

namespace MQC.ServerLibrary.Configurations
{
    public class QCConfiguration : IEntityTypeConfiguration<QC>
    {
        public void Configure(EntityTypeBuilder<QC> builder)
        {
            builder.ToTable("QCs");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.LabId).IsRequired();
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.QCProfileId).IsRequired();
            builder.Property(x => x.QCDate).IsRequired().HasColumnType("date").HasAnnotation("CheckConstraint", "QCDate <= GETDATE()");
            builder.Property(x => x.DateCreated).HasDefaultValueSql("GETDATE()");

            builder.HasOne(o => o.User).WithMany(m => m.QCs).HasForeignKey(i => i.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(o => o.Laboratory).WithMany(m => m.QCs).HasForeignKey(i => i.LabId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.QCProfile).WithMany(m => m.QCs).HasForeignKey(i => i.QCProfileId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
