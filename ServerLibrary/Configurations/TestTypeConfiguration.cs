﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MQC.BaseLibrary.Entities;

namespace MQC.ServerLibrary.Configurations
{
    public class TestTypeConfiguration : IEntityTypeConfiguration<TestType>
    {
        public void Configure(EntityTypeBuilder<TestType> builder)
        {
            builder.ToTable("TestTypes");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.TypeName).IsRequired().HasMaxLength(200);
        }
    }
}
