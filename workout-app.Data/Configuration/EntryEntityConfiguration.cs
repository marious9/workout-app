using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using workout_app.Core.Domain;

namespace workout_app.Data.Configuration
{
    public class EntryEntityConfiguration : IEntityTypeConfiguration<Entry>
    {
        public void Configure(EntityTypeBuilder<Entry> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasOne(e => e.Exercise)
                    .WithMany(e => e.Entries);            
        }
    }
}
