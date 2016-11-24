using AutoMapper;
using System.Linq;
using WorkData.Dto.Entity;
using WorkData.EF.Domain.Entity;

namespace WorkData.Dto.Profiles
{
    public class ContentIntFieldDtoProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<ContentIntFieldDto, ContentIntField>()
             .ForMember(d => d.Content, map => map.MapFrom(m => m.Content));

            CreateMap<ContentIntField, ContentIntFieldDto>()
              .ForMember(d => d.Content, map => map.MapFrom(m => m.Content));

        }
    }
}