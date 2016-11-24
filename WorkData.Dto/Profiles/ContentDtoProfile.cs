using AutoMapper;
using System.Linq;
using WorkData.Dto.Entity;
using WorkData.EF.Domain.Entity;

namespace WorkData.Dto.Profiles
{
    public class ContentDtoProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<ContentDto, Content>()
             .ForMember(d => d.Category, map => map.MapFrom(m => m.Category))
             .ForMember(d => d.Model, map => map.MapFrom(m => m.Model))
             .ForMember(d => d.ContentDescriptionFields, map => map.MapFrom(m => m.ContentDescriptionFields.ToList()))
             .ForMember(d => d.ContentDoubleFields, map => map.MapFrom(m => m.ContentDoubleFields.ToList()))
             .ForMember(d => d.ContentIntFields, map => map.MapFrom(m => m.ContentIntFields.ToList()))
             .ForMember(d => d.ContentStringFields, map => map.MapFrom(m => m.ContentStringFields.ToList()))
             .ForMember(d => d.ContentTextFields, map => map.MapFrom(m => m.ContentTextFields.ToList()))
             .ForMember(d => d.ContentTimeFields, map => map.MapFrom(m => m.ContentTimeFields.ToList()))
             ;

            CreateMap<Content, ContentDto>()
             .ForMember(d => d.Category, map => map.MapFrom(m => m.Category))
             .ForMember(d => d.Model, map => map.MapFrom(m => m.Model))
             .ForMember(d => d.ContentDescriptionFields, map => map.MapFrom(m => m.ContentDescriptionFields.ToList()))
             .ForMember(d => d.ContentDoubleFields, map => map.MapFrom(m => m.ContentDoubleFields.ToList()))
             .ForMember(d => d.ContentIntFields, map => map.MapFrom(m => m.ContentIntFields.ToList()))
             .ForMember(d => d.ContentStringFields, map => map.MapFrom(m => m.ContentStringFields.ToList()))
             .ForMember(d => d.ContentTextFields, map => map.MapFrom(m => m.ContentTextFields.ToList()))
             .ForMember(d => d.ContentTimeFields, map => map.MapFrom(m => m.ContentTimeFields.ToList()))
              ;

        }
    }
}