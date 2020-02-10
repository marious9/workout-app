using Microsoft.EntityFrameworkCore;
using workout_app.Core.Domain;

namespace workout_app.Data.Configuration
{
    public class WorkoutAppDbContext : DbContext
    {
        public WorkoutAppDbContext(DbContextOptions<WorkoutAppDbContext> options) : base(options)
        {
        }

        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Entry> Entries { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ExerciseEntityConfiguration());
            modelBuilder.ApplyConfiguration(new EntryEntityConfiguration());
            modelBuilder.ApplyConfiguration(new SessionEntityConfiguration());
            modelBuilder.ApplyConfiguration(new TrainingEntityConfiguration());
            modelBuilder.ApplyConfiguration(new SubcategoryEntityConfiguration());
        }

    }
}
