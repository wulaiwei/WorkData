using AutoMapper;
using System.Linq;
using WorkData.Dto.Entity;
using WorkData.EF.Domain.Entity;

namespace WorkData.Dto.Profiles
{
    public class ContentStringFieldDtoProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<ContentStringFieldDto, ContentStringField>()
             .ForMember(d => d.Content, map => map.MapFrom(m => m.Content));

            CreateMap<ContentStringField, ContentStringFieldDto>()
              .ForMember(d => d.Content, map => map.MapFrom(m => m.Content));

        }
    }
}