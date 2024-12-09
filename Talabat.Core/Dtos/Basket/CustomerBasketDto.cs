using Talabat.Core.Entities;

namespace Talabat.Core.Dtos.Basket;

public class CustomerBasketDto
{
    public string Id { get; set; }
    public List<BasketItem> Items { get; set; }
}