namespace Talabat.Core.Entities;

public class CustomerBasket
{
    public string Id { get; set; }
    public List<BasketItem> Items { get; set; }
    
    public CustomerBasket(string id)
    {
        Id = id;
        Items = new List<BasketItem>();
    }
    
}