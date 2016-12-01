using AutoMapper;
using System.Linq;
using WorkData.Dto.Entity;
using WorkData.EF.Domain.Entity;

namespace WorkData.Dto.Profiles
{
    public class ResourceDtoProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<ResourceDto, Resource>()
                .ForMember(d => d.Roles, map => map.MapFrom(m => m.Roles.ToList()))
                .ForMember(d => d.Operations, map => map.MapFrom(m => m.Operations.ToList()))
                ;

            CreateMap<Resource, ResourceDto>()
                .ForMember(d => d.Roles, map => map.MapFrom(m => m.Roles.ToList()))
                .ForMember(d => d.Operations, map => map.MapFrom(m => m.Operations.ToList()))
                ;

        }
    }
}