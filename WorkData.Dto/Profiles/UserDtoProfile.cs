using AutoMapper;
using System.Linq;
using WorkData.Dto.Entity;
using WorkData.EF.Domain.Entity;

namespace WorkData.Dto.Profiles
{
    public class UserDtoProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<UserDto, User>()
                .ForMember(d => d.Roles, map => map.MapFrom(m => m.Roles.ToList()));
            //.ForMember(d => d.Resources, map => map.MapFrom(m => m.Resources.ToList()));

            CreateMap<User, UserDto>()
                .ForMember(d => d.Roles, map => map.MapFrom(m => m.Roles.ToList()));
                //.ForMember(d => d.Resources, map => map.MapFrom(m => m.Resources.ToList()));
        }
    }
}