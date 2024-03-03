﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MQC.BaseLibrary.Entities;

namespace MQC.ServerLibrary.Configurations
{
    public class LaboratoryConfiguration : IEntityTypeConfiguration<Laboratory>
    {
        public void Configure(EntityTypeBuilder<Laboratory> builder)
        {
            builder.ToTable("Laboratories");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasIndex(x => x.OrganizationName);
            builder.Property(x => x.OrganizationName).IsRequired();
            builder.Property(x => x.LabName).IsRequired();
        }
    }
}
