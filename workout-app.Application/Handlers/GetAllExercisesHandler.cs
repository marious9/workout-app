using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using workout_app.Application.Mapping.Dto.Exercise;
using workout_app.Application.Queries;
using workout_app.Core.Domain;
using workout_app.Infrastructure.Configuration;

namespace workout_app.Application.Handlers
{
    public class GetAllExercisesHandler : IRequestHandler<GetAllExercisesQuery, List<ExerciseViewModel>>
    {
        private readonly WorkoutAppDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetAllExercisesHandler(WorkoutAppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<List<ExerciseViewModel>> Handle(GetAllExercisesQuery request, CancellationToken cancellationToken)
        {
            var exercises = await _dbContext.Exercises.ToListAsync(cancellationToken);

            var exerciseResponses = _mapper.Map<List<ExerciseViewModel>>(exercises);

            return exerciseResponses;
        }
    }
}
