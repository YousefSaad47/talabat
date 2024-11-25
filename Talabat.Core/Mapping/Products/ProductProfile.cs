using AutoMapper;
using Microsoft.Extensions.Configuration;
using Talabat.Core.Dtos.Products;
using Talabat.Core.Entities;

namespace Talabat.Core.Mapping.Products;

public class ProductProfile : Profile
{
    public ProductProfile(IConfiguration configuration)
    {
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.Name))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom(src => $"{configuration["baseUrl"]}/{src.PictureUrl}"));
        CreateMap<ProductBrand, CategoryBrandDto>();
        CreateMap<ProductCategory, CategoryBrandDto>();
    }
}