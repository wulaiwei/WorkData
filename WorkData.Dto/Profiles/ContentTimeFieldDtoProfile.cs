using AutoMapper;
using System.Linq;
using WorkData.Dto.Entity;
using WorkData.EF.Domain.Entity;

namespace WorkData.Dto.Profiles
{
    public class ContentTimeFieldDtoProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<ContentTimeFieldDto, ContentTimeField>()
             .ForMember(d => d.Content, map => map.MapFrom(m => m.Content));

            CreateMap<ContentTimeField, ContentTimeFieldDto>()
              .ForMember(d => d.Content, map => map.MapFrom(m => m.Content));

        }
    }
}