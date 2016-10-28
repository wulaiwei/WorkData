using AutoMapper;
using System.Linq;
using WorkData.Dto.Entity;
using WorkData.EF.Domain.Entity;

namespace WorkData.Dto.Profiles
{
    public class OperationDtoProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<OperationDto, Operation>()
                .ForMember(d => d.Privileges, map => map.MapFrom(m => m.Privileges.ToList()));

            CreateMap<Operation, OperationDto>()
                .ForMember(d => d.Privileges, map => map.MapFrom(m => m.Privileges.ToList()));
        }
    }
}