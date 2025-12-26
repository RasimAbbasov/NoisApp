using AutoMapper;
using Nois.Application.DTOs.AuthDtos;
using Nois.Application.DTOs.BasketDtos;
using Nois.Application.DTOs.CategoryDtos;
using Nois.Application.DTOs.CategoryDTOs;
using Nois.Application.DTOs.ColorDtos;
using Nois.Application.DTOs.OrderDtos;
using Nois.Application.DTOs.ProductDtos;
using Nois.Application.DTOs.ProductStockDtos;
using Nois.Application.DTOs.ProductVariantDtos;
using Nois.Application.DTOs.SizeDtos;
using Nois.Application.DTOs.WishlistDtos;
using Nois.Domain.Entities;
using Nois.Domain.Entities.Identity;

namespace Nois.Application.Profiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            //Category
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();
            CreateMap<Category, CategorySummaryDto>().ReverseMap();

            //Color
            CreateMap<CreateColorDto, Color>();
            CreateMap<UpdateColorDto, Color>();
            CreateMap<Color, ColorSummaryDto>().ReverseMap();
            //Size
            CreateMap<CreateSizeDto, Size>();
            CreateMap<UpdateSizeDto, Size>();
            CreateMap<Size, SizeSummaryDto>().ReverseMap();

            //Product
            CreateMap<Product, ProductSummaryDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ReverseMap();

            CreateMap<Product, ProductDetailDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.ProductVariantDtos, opt => opt.MapFrom(src => src.ProductVariants));


            CreateMap<CreateProductDto, Product>()
                       .ForMember(dest => dest.BlobName, opt => opt.Ignore())// we set BlobName manually
                       .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

            CreateMap<UpdateProductDto, Product>()
                     .ForMember(dest => dest.BlobName, opt => opt.Ignore())
                     .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

            //ProductVariant
            CreateMap<ProductVariant, ProductVariantSummaryDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.SizeName, opt => opt.MapFrom(src => src.Size.Name))
                .ForMember(dest => dest.ColorName, opt => opt.MapFrom(src => src.Color.Name));

            CreateMap<CreateProductVariantDto, ProductVariant>()
           .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

            CreateMap<UpdateProductVariantDto, ProductVariant>()
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

            //ProductStock
            CreateMap<ProductStock, ProductStockSummaryDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductVariant.Product.Name))
                .ForMember(dest => dest.ColorName, opt => opt.MapFrom(src => src.ProductVariant.Color.Name))
                .ForMember(dest => dest.SizeName, opt => opt.MapFrom(src => src.ProductVariant.Size.Name));


            CreateMap<CreateProductStockDto, ProductStock>()
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

            CreateMap<UpdateProductStockDto, ProductStock>()
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

            //User (Check)
            CreateMap<RegisterDto, AppUser>();


            //Wishlist 
            CreateMap<Wishlist, WishlistItemDto>()
              .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
              .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Product.Name))
              .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Product.BlobName))
              .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price));

            CreateMap<CreateWishlistItemDto, Wishlist>();


            //Basket 
            CreateMap<Basket, BasketDto>()
           .ForMember(d => d.TotalPrice, o => o.MapFrom(s => s.Items.Sum(i => i.UnitPrice * i.Quantity)));

            CreateMap<BasketItem, BasketItemDto>();

            //Order 
            CreateMap<Order, OrderDto>()
                .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()));

            CreateMap<OrderItem, OrderItemDto>();

            CreateMap<Order, OrderAdminDto>()
              .ForMember(dest => dest.BuyerUserName, opt => opt.MapFrom(src => src.User.UserName)).ReverseMap();

        }

    }
}
