using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Talabat.Apis.Attributes;
using Talabat.Core.Dtos.Products;
using Talabat.Core.Service.Contract;

namespace Talabat.Apis.Controllers;

public class ProductsController : BaseApiController
{
   private readonly IProductService _productService;

   public ProductsController(IProductService productService)
   {
      _productService = productService;
   }
   
   [HttpGet]
   [Cache(100)]
   [Authorize]
   public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts(
       [FromQuery]string? sort,
       [FromQuery]int? brandId,
       [FromQuery]int? typeId,
       [FromQuery]int? pageSize,
       [FromQuery]int? pageIndex
       )
   {
       var products = await _productService.GetAllProductsAsync(sort, brandId, typeId, pageSize, pageIndex);
       
       return Ok(products);
   }

   [HttpGet("categories")]
   public async Task<ActionResult<IEnumerable<CategoryBrandDto>>> GetAllCategories()
   {
       var categories = await _productService.GetAllCategoriesAsync();
       
       return Ok(categories);
   }
   
   [HttpGet("brands")]
   public async Task<ActionResult<IEnumerable<CategoryBrandDto>>> GetAllBrands()
   {
       var brands = await _productService.GetAllBrandsAsync();
       
       return Ok(brands);
   }
   
   [HttpGet("{id}")]
   public async Task<ActionResult<ProductDto>> GetProductById(int? id)
   {
       if (id == null)
       {
           return BadRequest("Invalid Id");
       }
       
       var product = await _productService.GetProductById(id.Value);

       if (product == null)
       {
           return NotFound(new { message = "Product not found", statusCode = 404 });
       }

       return Ok(product);
   }
}