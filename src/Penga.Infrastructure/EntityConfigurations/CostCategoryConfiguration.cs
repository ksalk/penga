using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Penga.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Penga.Infrastructure.EntityConfigurations
{
    public class CostCategoryConfiguration : IEntityTypeConfiguration<CostCategory>
    {
        public void Configure(EntityTypeBuilder<CostCategory> builder)
        {
            builder.ToTable("CostCategory");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
