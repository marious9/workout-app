using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using workout_app.Application.Mapping.Profiles;

namespace workout_app.Api.IoC
{
    public static class ApplicationIoC
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc => 
            {
                mc.AddProfile(new ExerciseProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }
    }
}
