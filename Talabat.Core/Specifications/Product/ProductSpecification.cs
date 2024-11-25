namespace Talabat.Core.Specifications.Product;

public class ProductSpecification : BaseSpecification<Entities.Product, int>
{
    public ProductSpecification(int id):base(p => p.Id == id)
    {
        AddIncludes();
    }
    
    public ProductSpecification(string? sort, int? brandId, int? typeId, int pageSize, int pageIndex)
        : base(p => 
            (!brandId.HasValue || brandId == p.BrandId) && (!typeId.HasValue || typeId == p.CategoryId)
        )
    {
        if (!string.IsNullOrEmpty(sort))
        {
            switch (sort)
            {
                case "priceAsc":
                    AddOrderBy(p => p.Price);
                    break;
                case "priceDesc":
                    AddOrderByDesc(p => p.Price);
                    break;
                default:
                    AddOrderBy(p => p.Name);
                    break;
            }
        }
        AddIncludes();
        
        ApplyPagination(pageSize * (pageIndex - 1), pageSize);
    }
    
    private void AddIncludes()
    {
        Includes.Add(p => p.Brand);
        Includes.Add(p => p.Category);
    }
}