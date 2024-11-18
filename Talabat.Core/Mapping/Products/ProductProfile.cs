using AutoMapper;
using Talabat.Core.Dtos.Products;
using Talabat.Core.Entities;

namespace Talabat.Core.Mapping.Products;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.Name))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
        CreateMap<ProductBrand, CategoryBrandDto>();
        CreateMap<ProductCategory, CategoryBrandDto>();
    }
}