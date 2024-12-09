using System.Text.Json;
using StackExchange.Redis;
using Talabat.Core.Service.Contract;

namespace Talabat.Service.Services.Cache;

public class CacheService : ICacheService
{
    private readonly IDatabase _database;
    public CacheService(IConnectionMultiplexer redis)
    {
        _database = redis.GetDatabase();
    }
    
    public async Task<string> GetCacheAsync(string key)
    {
        var cacheResponse = await _database.StringGetAsync(key);   
        
        if (cacheResponse.IsNullOrEmpty)
            return null;
        
        return cacheResponse.ToString();
    }

    public async Task SetCacheAsync(string key, object response, TimeSpan expireDate)
    {
        if (response is null)
            return;
        
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        
         await _database.StringSetAsync(key, JsonSerializer.Serialize(response, options), expireDate);
    }
}