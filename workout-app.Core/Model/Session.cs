using System;
using System.Collections.Generic;
using System.Text;

namespace workout_app.Core.Model
{
    public class Session
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public ICollection<Training> Trainings { get; set; }
    }
}
