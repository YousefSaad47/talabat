using System.ComponentModel.DataAnnotations.Schema;

namespace Talabat.Core.Entities;

public class Product : BaseEntity<int>
{
    public string Name { get; set; } = null!;
    
    public string Description { get; set; } = null!;
    
    public string PictureUrl { get; set; } = null!;
    
    public decimal Price { get; set; }
    
    // Product & Brand
    public int? BrandId { get; set; } // FK Column => ProductBrand 
    
    public ProductBrand Brand { get; set; } = null!; // Navigational property [ONE]
    
    // Product & Category
    public int? CategoryId { get; set; } // FK Column => ProductCategory
    
    public ProductCategory Category { get; set; } = null!; // Navigational property [ONE]
}