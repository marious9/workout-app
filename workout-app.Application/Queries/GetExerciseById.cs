using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using workout_app.Application.Mapping.Dto.Exercise;
using workout_app.Core.Domain;
using workout_app.Core.Domain.Helpers;
using workout_app.Infrastructure.Configuration;

namespace workout_app.Application.Queries
{
    public static class GetExerciseById
    {
        public class GetExerciseByIdQuery : IRequest<ExerciseDto>
        {
            public int Id { get; }

            public GetExerciseByIdQuery(int id)
            {
                Id = id;
            }
        }

        public class GetExerciseByIdHandler : IRequestHandler<GetExerciseByIdQuery, ExerciseDto>
        {
            private readonly WorkoutAppDbContext _dbContext;
            private readonly IMapper _mapper;

            public GetExerciseByIdHandler(WorkoutAppDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }

            public async Task<ExerciseDto> Handle(GetExerciseByIdQuery request, CancellationToken cancellationToken)
            {
                Exercise exercise = await _dbContext.Exercises.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (exercise == null)
                    throw new NotFoundRuleValidationException("Exercise not found");

                var exerciseResponse = _mapper.Map<ExerciseDto>(exercise);

                return exerciseResponse;
            }

            public class GetExerciseByIdQueryValidator : AbstractValidator<GetExerciseByIdQuery>
            {
                public GetExerciseByIdQueryValidator()
                {
                    RuleFor(x => x.Id)
                        .NotEmpty();
                }
            }
        }
    }
}
