using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MQC.BaseLibrary.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace MQC.ServerLibrary.Configurations
{
    public class ResultConfiguration : IEntityTypeConfiguration<Result>
    {
        public void Configure(EntityTypeBuilder<Result> builder)
        {
            builder.ToTable("Results");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.QCId).IsRequired();
            builder.Property(x => x.StandardDetailId).IsRequired();
            builder.Property(x => x.LastUpdated).IsRequired().HasColumnType("datetime").ValueGeneratedOnAddOrUpdate();

            builder.HasOne(o => o.QC).WithMany(m => m.Results).HasForeignKey(i => i.QCId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.StandardDetail).WithMany(m => m.Results).HasForeignKey(i => i.StandardDetailId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.LotTest).WithMany(m => m.Results).HasForeignKey(i => i.LotTestId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(o => o.User).WithMany(m => m.Results).HasForeignKey(i => i.UpdatedByUserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
