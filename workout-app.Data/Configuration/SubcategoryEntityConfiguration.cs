using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using workout_app.Core.Domain;

namespace workout_app.Data.Configuration
{
    public class SubcategoryEntityConfiguration : IEntityTypeConfiguration<Subcategory>
    {
        public void Configure(EntityTypeBuilder<Subcategory> builder)
        {
            builder.HasKey(s => s.Id);
            builder.HasOne(s => s.Exercise).WithMany(e => e.Subcategories);
            builder.Property(e => e.Category).HasConversion<string>();
        }
    }
}
