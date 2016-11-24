using AutoMapper;
using System.Linq;
using WorkData.Dto.Entity;
using WorkData.EF.Domain.Entity;

namespace WorkData.Dto.Profiles
{
    public class ContentTextFieldDtoProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<ContentTextFieldDto, ContentTextField>()
             .ForMember(d => d.Content, map => map.MapFrom(m => m.Content));

            CreateMap<ContentTextField, ContentTextFieldDto>()
              .ForMember(d => d.Content, map => map.MapFrom(m => m.Content));

        }
    }
}