namespace Talabat.Core.Specifications.Product;

public class ProductSpecification : BaseSpecification<Entities.Product, int>
{
    public ProductSpecification(int id):base(p => p.Id == id)
    {
        AddIncludes();
    }
    
    public ProductSpecification(string? sort)
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
    }
    
    private void AddIncludes()
    {
        Includes.Add(p => p.Brand);
        Includes.Add(p => p.Category);
    }
}