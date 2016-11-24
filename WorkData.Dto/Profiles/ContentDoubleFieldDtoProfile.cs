using AutoMapper;
using System.Linq;
using WorkData.Dto.Entity;
using WorkData.EF.Domain.Entity;

namespace WorkData.Dto.Profiles
{
    public class ContentDoubleFieldDtoProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<ContentDoubleFieldDto, ContentDoubleField>()
             .ForMember(d => d.Content, map => map.MapFrom(m => m.Content));

            CreateMap<ContentDoubleField, ContentDoubleFieldDto>()
              .ForMember(d => d.Content, map => map.MapFrom(m => m.Content));

        }
    }
}