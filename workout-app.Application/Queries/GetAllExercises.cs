using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using workout_app.Application.Mapping.Dto.Exercise;
using workout_app.Infrastructure.Configuration;

namespace workout_app.Application.Queries
{
    public static class GetAllExercises
    {
        public class GetAllExercisesQuery : IRequest<List<ExerciseDto>>
        {
        }

        public class GetAllExercisesHandler : IRequestHandler<GetAllExercisesQuery, List<ExerciseDto>>
        {
            private readonly WorkoutAppDbContext _dbContext;
            private readonly IMapper _mapper;
            public GetAllExercisesHandler(WorkoutAppDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }
            public async Task<List<ExerciseDto>> Handle(GetAllExercisesQuery request, CancellationToken cancellationToken)
            {
                var exercises = await _dbContext.Exercises.ToListAsync(cancellationToken);

                var exerciseResponses = _mapper.Map<List<ExerciseDto>>(exercises);

                return exerciseResponses;
            }
        }
    }
}
