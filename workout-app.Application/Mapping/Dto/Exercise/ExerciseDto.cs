using System;
using System.Collections.Generic;
using System.Text;

namespace workout_app.Application.Mapping.Dto.Exercise
{
    public class ExerciseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string VideoLink { get; set; }
        public string Category { get; set; }        
    }
}
