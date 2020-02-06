using System;
using System.Collections.Generic;
using System.Text;

namespace workout_app.Core.Domain
{
    public class Session
    {
        public int Id { get; }
        public DateTime DateTime { get; set; }
        public ICollection<Training> Trainings { get; set; }
    }
}
