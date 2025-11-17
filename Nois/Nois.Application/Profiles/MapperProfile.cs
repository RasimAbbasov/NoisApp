using AutoMapper;
using Nois.Application.DTOs.CategoryDTOs;
using Nois.Application.DTOs.ColorDtos;
using Nois.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nois.Application.Profiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<CategoryDto, Category>().ReverseMap();
            CreateMap<CreateColorDto, Color>();
            CreateMap<ColorDto,Color>().ReverseMap();
        }

    }
}
