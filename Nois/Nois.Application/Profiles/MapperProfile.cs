using AutoMapper;
using Nois.Application.DTOs.CategoryDtos;
using Nois.Application.DTOs.CategoryDTOs;
using Nois.Application.DTOs.ColorDtos;
using Nois.Application.DTOs.ProductDtos;
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
            CreateMap<UpdateCategoryDto, Category>();
            CreateMap<CategorySummaryDto, Category>().ReverseMap();

            //Color
            CreateMap<CreateColorDto, Color>();
            CreateMap<UpdateColorDto, Color>();
            CreateMap<ColorSummaryDto,Color>().ReverseMap();
            //Size
            CreateMap<CreateSizeDto, Size>();
            CreateMap<UpdateSizeDto, Size>();
            CreateMap<SizeSummaryDto,Size>().ReverseMap();

            //Product
            CreateMap<ProductSummaryDto, Product>().ReverseMap();

            CreateMap<CreateProductDto, Product>()
                       .ForMember(dest => dest.BlobName, opt => opt.Ignore())// we set BlobName manually
                       .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                       .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
            CreateMap<UpdateProductDto, Product>()
                     .ForMember(dest => dest.BlobName, opt => opt.Ignore())
                     .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                     .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
        }

    }
}
