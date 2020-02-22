using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using workout_app.Application.Mapping.Dto.Exercise;
using workout_app.Core.Domain;

namespace workout_app.Application.Queries
{
    public class GetAllExercisesQuery : IRequest<List<ExerciseViewModel>>
    {
    }
}
