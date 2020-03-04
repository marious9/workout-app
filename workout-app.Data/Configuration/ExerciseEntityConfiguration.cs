using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using workout_app.Core.Domain;

namespace workout_app.Infrastructure.Configuration
{
    public class ExerciseEntityConfiguration : IEntityTypeConfiguration<Exercise>
    {
        public void Configure(EntityTypeBuilder<Exercise> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Category).IsRequired();
            builder.Property(e => e.Description).HasMaxLength(500);
            builder.Property(e => e.Name)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(e => e.Category).HasConversion<string>();
            builder.HasMany(e => e.Subcategories).WithOne(s => s.Exercise);
        }
    }
}
