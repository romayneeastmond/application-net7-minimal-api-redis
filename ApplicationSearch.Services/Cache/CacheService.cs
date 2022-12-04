using StackExchange.Redis;
using System.Net;

namespace ApplicationSearch.Services.Cache
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase _cache = default!;
        private readonly EndPoint[] _endPoints = default!;
        private IServer _server = default!;

        public CacheService() { }

        public CacheService(string cacheConnection)
        {
            try
            {
                AzureCacheRedisService.CacheConnection = cacheConnection;

                _cache = AzureCacheRedisService.GetDatabase();
                _endPoints = AzureCacheRedisService.GetEndPoints();
                
                GetServer();
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

        public List<string> Scan(string pattern)
        {
            var results = new List<string>();

            foreach (var key in _server.Keys(pattern: pattern))
            {
                if (key.ToString() != null)
                {
                    results.Add(key.ToString());
                }
            }

            return results;
        }


        public async Task Set(string key, string value)
        {
            await _cache.StringSetAsync(key, value);
        }

        private void GetServer()
        {
            var endPoint = _endPoints.FirstOrDefault();
            var host = string.Empty;
            var port = 0;

            if (endPoint != null)
            {
                var dynamicHostProperty = endPoint.GetType().GetProperty("Host");
                var dynamicPortProperty = endPoint.GetType().GetProperty("Port");

                if (dynamicHostProperty != null)
                {
                    var temporaryValue = dynamicHostProperty.GetValue(endPoint, null);

                    if (temporaryValue != null)
                    {
                        host = temporaryValue.ToString();
                    }
                }

                if (dynamicPortProperty != null)
                {
                    var temporaryValue = dynamicPortProperty.GetValue(endPoint, null);

                    if (temporaryValue != null)
                    {
                        port = Convert.ToInt32(temporaryValue);
                    }
                }

                if (!string.IsNullOrWhiteSpace(host) && port != 0)
                {
                    _server = AzureCacheRedisService.GetServer(host, port);
                }
            }
        }
    }
}
