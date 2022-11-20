using StackExchange.Redis;

namespace ApplicationSearch.Services.Cache
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase _cache;

        public CacheService() { }

        public CacheService(string cacheConnection)
        {
            try
            {
                AzureCacheRedisService.CacheConnection = cacheConnection;

                _cache = AzureCacheRedisService.GetDatabase();
            }
            catch
            {
                //ignored
            }
        }

        public async Task Delete(string key)
        {
            await _cache.KeyDeleteAsync(key);
        }

        public async Task<string> Get(string key)
        {
            try
            {
                var value = await _cache.StringGetAsync(key);

                return value.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        public async Task Set(string key, string value)
        {
            await _cache.StringSetAsync(key, value);
        }
    }
}
