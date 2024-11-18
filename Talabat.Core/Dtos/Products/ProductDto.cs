namespace Talabat.Core.Dtos.Products;

public class ProductDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string PictureUrl { get; set; } = null!;

    public decimal Price { get; set; }

    // Product & Brand
    public int? BrandId { get; set; } // FK Column => ProductBrand 

    public string BrandName { get; set; } = null!; // Navigational property [ONE]

    // Product & Category
    public int? CategoryId { get; set; } // FK Column => ProductCategory

    public string CategoryName { get; set; } = null!; // Navigational property [ONE]}
}