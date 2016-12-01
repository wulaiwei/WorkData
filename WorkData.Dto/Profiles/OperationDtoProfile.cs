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
             .ForMember(d => d.Resources, map => map.MapFrom(m => m.Resources.ToList()));
             //.MaxDepth(1);

            CreateMap<Operation, OperationDto>()
              .ForMember(d => d.Resources, map => map.MapFrom(m => m.Resources.ToList()));
              //.MaxDepth(1);
        }
    }
}