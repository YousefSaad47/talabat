
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Talabat.Apis.Errors;
using Talabat.Core.Dtos.Basket;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;

namespace Talabat.Apis.Controllers;

public class BasketController : BaseApiController
{
    private readonly IBasketRepository _basketRepository;
    private readonly IMapper _mapper;

    public BasketController(IBasketRepository basketRepository, IMapper mapper)
    {
        _basketRepository = basketRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<CustomerBasket>> GetBasket(string? id)
    {
        if (id is null) 
            return BadRequest(new ApiResponse(400, "Invalid Id"));
        
        var basket = await _basketRepository.GetBasketAsync(id);

        return Ok(basket ?? new CustomerBasket(id));
    }
    
    [HttpPost]
    public async Task<ActionResult<CustomerBasket>> CreateOrUpdateBasket(CustomerBasketDto basket)
    {
        var mappedBasket = _mapper.Map<CustomerBasket>(basket);
        
        var createdOrUpdatedBasket = await _basketRepository.UpdateBasketAsync(mappedBasket);
        
        if (createdOrUpdatedBasket is null)
            return BadRequest(new ApiResponse(400));
        
        return Ok(createdOrUpdatedBasket);
    }
    
    [HttpDelete]
    public async Task DeleteBasket(string basketId)
    {
        await _basketRepository.DeleteBasketAsync(basketId);
    }
}