using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using workout_app.Core.Domain;
using workout_app.Core.Domain.Helpers;
using workout_app.Infrastructure.Configuration;

namespace workout_app.Application.Commands
{
    public static class CreateExercise
    {
        public class CreateExerciseCommand : IRequest<int>
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string VideoLink { get; set; }
            public string Category { get; set; }
            public ICollection<string> Subcategories { get; set; }
        }

        public class CreateExerciseHandler : IRequestHandler<CreateExerciseCommand, int>
        {
            private readonly WorkoutAppDbContext _dbContext;

            public CreateExerciseHandler(WorkoutAppDbContext dbContext)
            {
                _dbContext = dbContext;

            }
            public async Task<int> Handle(CreateExerciseCommand request, CancellationToken cancellationToken)
            {
                bool exerciseAlreadyExists = await _dbContext.Exercises.AnyAsync(x => x.Name == request.Name, cancellationToken);

                if (exerciseAlreadyExists)
                    throw new BusinessRuleValidationException("Exercise with this name already exists");

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

                return createdExercise.Entity.Id;
            }
        }

        public class CreateExerciseCommandValidator : AbstractValidator<CreateExerciseCommand>
        {
            public CreateExerciseCommandValidator()
            {
                RuleFor(x => x.Description)
                    .MaximumLength(500);

                RuleFor(x => x.Name)
                    .MaximumLength(100)
                    .NotEmpty();

                RuleFor(x => x.Category)
                    .IsEnumName(typeof(Category))
                    .NotEmpty();
            }
        }
    }
}
