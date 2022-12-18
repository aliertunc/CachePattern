
using Microsoft.Extensions.Caching.Memory;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CachePattern.Unit.Test
{
    public class CacheManagerTests
    {
        private readonly IMemoryCache _cache;
        CacheManager.CacheManager manager;


        public CacheManagerTests()
        {
            _cache = new MemoryCache(new MemoryCacheOptions());
            manager = new CacheManager.CacheManager(_cache);
        }
        [Fact]
        public void AddCacheTest()
        {
            CacheManager.CacheManager manager = new CacheManager.CacheManager(_cache);
            string keyName = "theKeyName";
            string keyValue = "theKeyValue";

            manager.AddCache(keyName, keyValue, 5);
            var cacheValue = manager.GetCache(keyName);

            Assert.NotNull(cacheValue);
            Assert.Equal(keyValue, cacheValue);
        }
        [Fact]
        public async Task AddCacheTestAsync()
        {
            string keyName = "theKeyName";
            string keyValue = "theKeyValue";

            await manager.AddCacheAsync(keyName, keyValue, 5);
            var cacheValue = manager.GetCache(keyName);

            Assert.NotNull(cacheValue);
            Assert.Equal(keyValue, cacheValue);
        }

        [Fact]
        public void RemoveCacheTest()
        { 
            string keyName = "theKeyName";
            string keyValue = "theKeyValue";

            manager.AddCache(keyName, keyValue, 5);
            var cacheValue = manager.GetCache(keyName);
            manager.RemoveCache(keyName);
            var removedCacheValue = manager.GetCache(keyName);

            Assert.NotNull(cacheValue);
            Assert.Null(removedCacheValue);
            Assert.Equal(keyValue, cacheValue);
        }
        [Fact]
        public async Task GetCacheAsyncTest()
        { 
            string keyName = "theKeyName";
            string keyValue = "theKeyValue";

            manager.AddCache(keyName, keyValue, 5);
            var cacheValue = await manager.GetCacheAsync(keyName);

            Assert.NotNull(cacheValue);
            Assert.Equal(keyValue, cacheValue);
        }

        [Fact]
        public void GetTestAfterExpire()
        { 
            string keyName = "theKeyName";
            string keyValue = "theKeyValue";

            manager.AddCache(keyName, keyValue, 5);
            var cacheValue = manager.GetCache(keyName);
            Thread.Sleep(5000);
            var cacheValueExpired = manager.GetCache(keyName);

            Assert.Null(cacheValueExpired);
            Assert.NotNull(cacheValue);
            Assert.Equal(keyValue, cacheValue);
        }
    }
}
