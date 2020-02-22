using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using workout_app.Application.Mapping.Dto.Exercise;
using workout_app.Core.Domain;

namespace workout_app.Application.Commands
{
    public class CreateExerciseCommand: IRequest<Exercise>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string VideoLink { get; set; }
        public string Category { get; set; }
        public ICollection<string> Subcategories { get; set; }
    }
}
