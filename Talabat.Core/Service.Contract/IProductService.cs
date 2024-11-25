using Talabat.Core.Dtos.Products;

namespace Talabat.Core.Service.Contract;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllProductsAsync(string? sort, int? brandId, int? typeId, int? pageSize, int? pageIndex);

    Task<IEnumerable<CategoryBrandDto>> GetAllCategoriesAsync();
    
    Task<IEnumerable<CategoryBrandDto>> GetAllBrandsAsync();

    Task<ProductDto> GetProductById(int id);
}