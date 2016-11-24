using AutoMapper;
using System.Linq;
using WorkData.Dto.Entity;
using WorkData.EF.Domain.Entity;

namespace WorkData.Dto.Profiles
{
    public class ModelDtoProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<ModelDto, Model>()
             .ForMember(d => d.Contents, map => map.MapFrom(m => m.Contents.ToList()))
             .ForMember(d => d.Categorys, map => map.MapFrom(m => m.Categorys.ToList()))
             .ForMember(d => d.ModelFields, map => map.MapFrom(m => m.ModelFields.ToList()))
             ;

            CreateMap<Model, ModelDto>()
             .ForMember(d => d.Contents, map => map.MapFrom(m => m.Contents.ToList()))
             .ForMember(d => d.Categorys, map => map.MapFrom(m => m.Categorys.ToList()))
             .ForMember(d => d.ModelFields, map => map.MapFrom(m => m.ModelFields.ToList()))
              ;

        }
    }
}