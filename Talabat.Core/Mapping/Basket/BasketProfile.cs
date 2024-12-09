using AutoMapper;
using Talabat.Core.Dtos.Basket;
using Talabat.Core.Entities;

namespace Talabat.Core.Mapping.Basket;

public class BasketProfile : Profile
{
    public BasketProfile()
    {
        CreateMap<CustomerBasketDto, CustomerBasket>();
    }
}