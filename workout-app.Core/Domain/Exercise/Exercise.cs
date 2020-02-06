using System;
using System.Collections.Generic;
using System.Text;

namespace workout_app.Core.Domain
{
    public class Exercise
    {
        public int Id { get; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string VideoLink { get; set; }
        public ICollection<Entry> Entries { get; set; }
        public Category Category { get; set; }
        public ICollection<Category> Subcategories { get; set; }
    }
}
