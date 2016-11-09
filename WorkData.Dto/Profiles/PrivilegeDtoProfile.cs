using AutoMapper;
using System.Linq;
using WorkData.Dto.Entity;
using WorkData.EF.Domain.Entity;

namespace WorkData.Dto.Profiles
{
    public class PrivilegeDtoProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<PrivilegeDto, Privilege>()
                //.ForMember(d => d.Users, map => map.MapFrom(m => m.Users.ToList()))
                .ForMember(d => d.Roles, map => map.MapFrom(m => m.Roles.ToList()));

            CreateMap<Privilege, PrivilegeDto>()
                //.ForMember(d => d.Users, map => map.MapFrom(m => m.Users.ToList()))
                .ForMember(d => d.Roles, map => map.MapFrom(m => m.Roles.ToList()));
        }
    }
}