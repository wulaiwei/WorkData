using AutoMapper;
using EFModel.Entity;
using System.Linq;
using WorkData.Dto.Entity;

namespace WorkData.Dto.Profiles
{
    public class ResourceDtoProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<ResourceDto, Resource>()
                .ForMember(d => d.Privileges, map => map.MapFrom(m => m.Privileges.ToList()));

            CreateMap<Resource, ResourceDto>()
                .ForMember(d => d.Privileges, map => map.MapFrom(m => m.Privileges.ToList()));
        }
    }
}