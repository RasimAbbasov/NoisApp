using AutoMapper;
using Nois.Application.DTOs.CategoryDTOs;
using Nois.Application.DTOs.ColorDtos;
using Nois.Application.DTOs.SizeDtos;
using Nois.Domain.Entities;

namespace Nois.Application.Profiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            //Category
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<CategorySummaryDto, Category>().ReverseMap();
            //Color
            CreateMap<CreateColorDto, Color>();
            CreateMap<ColorSummaryDto,Color>().ReverseMap();
            //Size
            CreateMap<CreateSizeDto, Size>();
            CreateMap<SizeSummaryDto,Size>().ReverseMap();
        } 

    }
}
