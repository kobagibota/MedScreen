using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MQC.BaseLibrary.Entities;

namespace MQC.ServerLibrary.Configurations
{
    public class AppLogConfiguration : IEntityTypeConfiguration<AppLog>
    {
        public void Configure(EntityTypeBuilder<AppLog> builder)
        {
            builder.ToTable("AppLogs");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Timestamp).HasDefaultValueSql("getdate()");
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.LogAction).IsRequired();
            builder.Property(x => x.Details).IsRequired();

            builder.HasOne(o=>o.User).WithMany(m=>m.AppLogs).HasForeignKey(i => i.UserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
