using System.Text.Json;
using StackExchange.Redis;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;

namespace Talabat.Repository.Repositories;

public class BasketRepository : IBasketRepository
{
    private readonly IDatabase _database;

    public BasketRepository(IConnectionMultiplexer redis)
    {
        _database = redis.GetDatabase();
    }
    
    public async Task<CustomerBasket?> GetBasketAsync(string basketId)
    {
        var basket = await  _database.StringGetAsync(basketId);
        
        return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(basket);
    }

    public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket)
    {
        var createOrUpdatedBasket = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(30));
        
        if (!createOrUpdatedBasket) return null;
        
        return await GetBasketAsync(basket.Id);
    }

    public async Task<bool> DeleteBasketAsync(string basketId)
    {
        return await _database.KeyDeleteAsync(basketId);
    }
}