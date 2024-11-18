using AutoMapper;
using Talabat.Core;
using Talabat.Core.Dtos.Products;
using Talabat.Core.Entities;
using Talabat.Core.Service.Contract;

namespace Talabat.Service.Services.Products;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        return _mapper.Map<IEnumerable<ProductDto>>(await _unitOfWork.Repository<Product, int>().GetAllAsync());
    }

    public async Task<IEnumerable<CategoryBrandDto>> GetAllCategoriesAsync()
    {
        return _mapper.Map<IEnumerable<CategoryBrandDto>>(await _unitOfWork.Repository<ProductCategory, int>().GetAllAsync());
    }

    public async Task<IEnumerable<CategoryBrandDto>> GetAllBrandsAsync()
    {
        return _mapper.Map<IEnumerable<CategoryBrandDto>>(await _unitOfWork.Repository<ProductBrand, int>().GetAllAsync());
    }

    public async Task<ProductDto> GetProductById(int id)
    {
        return _mapper.Map<ProductDto>(await _unitOfWork.Repository<Product, int>().GetByIdAsync(id));
    }
}