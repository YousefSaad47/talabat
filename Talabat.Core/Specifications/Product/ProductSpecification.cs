namespace Talabat.Core.Specifications.Product;

public class ProductSpecification : BaseSpecification<Entities.Product, int>
{
    public ProductSpecification(int id):base(p => p.Id == id)
    {
        AddIncludes();
    }
    
    public ProductSpecification()
    {
        AddIncludes();
    }
    
    private void AddIncludes()
    {
        Includes.Add(p => p.Brand);
        Includes.Add(p => p.Category);
    }
}