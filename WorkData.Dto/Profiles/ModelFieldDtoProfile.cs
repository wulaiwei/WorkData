using AutoMapper;
using System.Linq;
using WorkData.Dto.Entity;
using WorkData.EF.Domain.Entity;

namespace WorkData.Dto.Profiles
{
    public class ModelFieldDtoProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<ModelFieldDto, ModelField>()
             .ForMember(d => d.Models, map => map.MapFrom(m => m.Models.ToList()))
             ;

            CreateMap<ModelField, ModelFieldDto>()
                          .ForMember(d => d.Models, map => map.MapFrom(m => m.Models.ToList()))
              ;

        }
    }
}