using System;
using System.Runtime.Caching;

namespace oclockvn.Extensions.Helpers
{
    public class Cache : ICache
    {
        private static MemoryCache cache = MemoryCache.Default;

        public T GetOrSet<T>(string key, Func<T> factory, int expire)
        {
            var newValue = new Lazy<T>(factory);            
            var oldValue = cache.AddOrGetExisting(key, newValue, policy: new CacheItemPolicy { SlidingExpiration = TimeSpan.FromMinutes(expire) }) as Lazy<T>;

            try
            {
                return (oldValue ?? newValue).Value;
            }
            catch (Exception) // Exception thrown each time if something going wrong during value reading
            {
                cache.Remove(key);
                return default(T);
            }
        }

        public void Remove(string key)
        {
            if (cache.Contains(key))
                cache.Remove(key);
        }
    }
}
