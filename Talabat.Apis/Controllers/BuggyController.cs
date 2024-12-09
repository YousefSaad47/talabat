using Microsoft.AspNetCore.Mvc;
using Talabat.Apis.Errors;
using Talabat.Repository.Data.Contexts;

namespace Talabat.Apis.Controllers;

public class BuggyController : BaseApiController
{
    private readonly StoreContext _context;

    public BuggyController(StoreContext context)
    {
        _context = context;
    }
    
    [HttpGet("notfound")]
    public async Task<ActionResult> GetNotFoundRequest()
    {
        var brand = await _context.ProductBrands.FindAsync(100);
        
        if (brand is null)
            return NotFound(new ApiResponse(404));
        
        return Ok(brand);
    }
    
    [HttpGet("servererror")]
    public async Task<ActionResult> GetServerError()
    {
        var brand = await _context.ProductBrands.FindAsync(100);

        var brandToReturn = brand.ToString();
        
        return Ok(brandToReturn);
    }
    
    [HttpGet("badrequest")]
    public async Task<ActionResult> GetBadRequest()
    {
        return BadRequest(new ApiResponse(400));
    }
    
    [HttpGet("badrequest/{id}")]
    public async Task<ActionResult> GetValidationError(int id)
    {
        return Ok();
    }
    
   [HttpGet("unauthorized")]
    public async Task<ActionResult> GetUnauthorizedError()
    {
        return Unauthorized(new ApiResponse(401));
    }
}