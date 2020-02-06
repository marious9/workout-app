using System;
using System.Collections.Generic;
using System.Text;

namespace workout_app.Core.Domain
{
    public class Entry
    {
        public int Id { get; }
        public int Reps { get; set; }
        public double Weight { get; set; }
    }
}
