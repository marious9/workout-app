﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using workout_app.Application.Commands;
using workout_app.Application.Mapping.Dto.Exercise;
using workout_app.Core.Domain;
using workout_app.Infrastructure.Configuration;

namespace workout_app.Application.Handlers
{
    public class CreateExerciseHandler : IRequestHandler<CreateExerciseCommand, Exercise>
    {
        private readonly WorkoutAppDbContext _dbContext;

        public CreateExerciseHandler(WorkoutAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Exercise> Handle(CreateExerciseCommand request, CancellationToken cancellationToken)
        {
            var exercise = new Exercise
            {
                Name = request.Name,
                Description = request.Description,
                VideoLink = request.VideoLink,
                Category = (Category)Enum.Parse(typeof(Category), request.Category)
                //@TODO Add subcategories
            };

            var createdExercise = await _dbContext.Exercises.AddAsync(exercise, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return createdExercise.Entity;
        }
    }
}