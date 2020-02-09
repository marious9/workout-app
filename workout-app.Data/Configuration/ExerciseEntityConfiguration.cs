using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using workout_app.Core.Domain;

namespace workout_app.Data.Configuration
{
    public class ExerciseEntityConfiguration : IEntityTypeConfiguration<Exercise>
    {
        public void Configure(EntityTypeBuilder<Exercise> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Category).IsRequired();          
        }
    }
}
