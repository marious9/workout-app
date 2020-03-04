using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using workout_app.Application.Commands;
using workout_app.Core.Domain;

namespace workout_app.Application.Validation
{
    public class CreateExerciseCommandValidator: AbstractValidator<CreateExerciseCommand>
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
