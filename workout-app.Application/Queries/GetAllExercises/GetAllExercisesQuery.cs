using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using workout_app.Core.Domain;

namespace workout_app.Application.Queries.GetAllExercises
{
    public class GetAllExercisesQuery : IRequest<List<Exercise>>
    {
        
    }
}
