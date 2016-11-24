using AutoMapper;
using System.Linq;
using WorkData.Dto.Entity;
using WorkData.EF.Domain.Entity;

namespace WorkData.Dto.Profiles
{
    public class CategoryDtoProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<CategoryDto, Category>()
             .ForMember(d => d.Contents, map => map.MapFrom(m => m.Contents.ToList()));

            CreateMap<Category, CategoryDto>()
              .ForMember(d => d.Contents, map => map.MapFrom(m => m.Contents.ToList()));

        }
    }
}