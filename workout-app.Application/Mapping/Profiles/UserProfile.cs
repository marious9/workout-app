using AutoMapper;
using workout_app.Application.Mapping.Dto.User;
using workout_app.Core.Domain.User;

namespace workout_app.Application.Mapping.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}