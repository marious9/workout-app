using AutoMapper;
using AutoMapper.Configuration;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using workout_app.Application.Mapping.Profiles;
using workout_app.Application.PipelineBehaviours;
using workout_app.Application.Validation;

namespace workout_app.Api.IoC
{
    public static class ApplicationIoC
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            //mapper
            var mappingConfig = new MapperConfiguration(mc => 
            {
                mc.AddProfile(new ExerciseProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            //fluent validation
            services.AddValidatorsFromAssembly(typeof(CreateExerciseCommandValidator).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }
    }
}
