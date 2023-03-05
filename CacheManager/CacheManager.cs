using System; 
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory; 

namespace CacheManager
{
    public class CacheManager
    {
        private readonly IMemoryCache _cache;

        public CacheManager(IMemoryCache cache)
        {
            _cache = cache;
        }

        public void AddCache(string key, object value, double seconds)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromSeconds(seconds));
            _cache.Set(key, value, cacheEntryOptions);
        }
        public async Task AddCacheAsync(string key, object value, double seconds)
        {
            await Task.Run(() =>
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromSeconds(seconds));
                _cache.Set(key, value, cacheEntryOptions);
            });
        }
        public void RemoveCache(string key)
        {
            _cache.Remove(key);
        }

        public object GetCache(string key)
        {
            object value;
            if (_cache.TryGetValue(key, out value))
            {
                return value;
            }
            else
            {
                return null;
            }
        }
        public async Task<object> GetCacheAsync(string key)
        {
            object value = null;
            await Task.Run(() =>
            {
                if (!_cache.TryGetValue(key, out value))
                {
                    throw new ArgumentException("Value not found in cache.", nameof(key));
                }
            });

            return value;
        }
         
    }
}
