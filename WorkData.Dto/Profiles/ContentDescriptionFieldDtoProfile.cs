using AutoMapper;
using System.Linq;
using WorkData.Dto.Entity;
using WorkData.EF.Domain.Entity;

namespace WorkData.Dto.Profiles
{
    public class ContentDescriptionFieldDtoProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<ContentDescriptionFieldDto, ContentDescriptionField>()
             .ForMember(d => d.Content, map => map.MapFrom(m => m.Content));

            CreateMap<ContentDescriptionField, ContentDescriptionFieldDto>()
              .ForMember(d => d.Content, map => map.MapFrom(m => m.Content));

        }
    }
}