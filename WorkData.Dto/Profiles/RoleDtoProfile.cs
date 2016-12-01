using AutoMapper;
using System.Linq;
using WorkData.Dto.Entity;
using WorkData.EF.Domain.Entity;

namespace WorkData.Dto.Profiles
{
    public class RoleDtoProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<RoleDto, Role>()
                .ForMember(d => d.Users, map => map.MapFrom(m => m.Users.ToList()))
                .ForMember(d => d.Resources, map => map.MapFrom(m => m.Resources.ToList()));

            CreateMap<Role, RoleDto>()
                .ForMember(d => d.Users, map => map.MapFrom(m => m.Users.ToList()))
                .ForMember(d => d.Resources, map => map.MapFrom(m => m.Resources.ToList()));
        }
    }
}