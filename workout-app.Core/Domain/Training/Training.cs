using System;
using System.Collections.Generic;
using System.Text;

namespace workout_app.Core.Domain
{
    public class Training
    {
        public int Id { get; }
        public string Description { get; set; }
        public ICollection<Exercise> Exercises { get; set; }
    }
}
