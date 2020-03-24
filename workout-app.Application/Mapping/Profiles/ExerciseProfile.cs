using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using workout_app.Application.Mapping.Dto.Exercise;
using workout_app.Core.Domain;

namespace workout_app.Application.Mapping.Profiles
{
    public class ExerciseProfile : Profile
    {
        public ExerciseProfile()
        {
            CreateMap<Exercise, ExerciseDto>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.ToString()));
        }
    }
}
