using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using workout_app.Core.Domain;

namespace workout_app.Infrastructure.Configuration
{
    public class TrainingEntityConfiguration : IEntityTypeConfiguration<Training>
    {
        public void Configure(EntityTypeBuilder<Training> builder)
        {
            builder.HasKey(t => t.Id);

            builder.HasMany(t => t.Exercises)
                   .WithOne(t => t.Training);

            builder.HasOne(t => t.User)
                   .WithMany(t => t.Trainings);
        }
}
}
