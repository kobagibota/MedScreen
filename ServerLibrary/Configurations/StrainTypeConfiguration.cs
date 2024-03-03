﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MQC.BaseLibrary.Entities;

namespace MQC.ServerLibrary.Configurations
{
    public class StrainTypeConfiguration : IEntityTypeConfiguration<StrainType>
    {
        public void Configure(EntityTypeBuilder<StrainType> builder)
        {
            builder.ToTable("StrainTypes");
            builder.HasKey(x => new { x.StrainId, x.CategoryId });

            builder.HasOne(o => o.Category).WithMany(m => m.StrainTypes).HasForeignKey(i => i.CategoryId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(o => o.Strain).WithMany(m => m.StrainTypes).HasForeignKey(i => i.StrainId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
