using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using workout_app.Application.Queries.GetAllExercises;
using workout_app.Core.Domain;
using workout_app.Data.Configuration;

namespace workout_app.Application.Handlers
{
    public class GetAllExercisesHandler : IRequestHandler<GetAllExercisesQuery, List<Exercise>>
    {
        private readonly WorkoutAppDbContext _dbContext;
        public GetAllExercisesHandler(WorkoutAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Exercise>> Handle(GetAllExercisesQuery request, CancellationToken cancellationToken)
        {
            var exercises = await _dbContext.Exercises.ToListAsync(cancellationToken);

            return exercises;
        }
    }
}
