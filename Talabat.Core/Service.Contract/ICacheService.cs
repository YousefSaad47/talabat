namespace Talabat.Core.Service.Contract;

public interface ICacheService
{
    Task<string> GetCacheAsync(string key);
    Task SetCacheAsync(string key, object response, TimeSpan expireDate);
}