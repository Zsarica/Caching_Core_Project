using Cache.Common;
using System.Collections.Concurrent;

namespace CustomMemeoryCache.Policies
{
    public class LastInFirstOutEvictionPolicy : IEvictionPolicy
    {
        public bool Execute(ConcurrentDictionary<string, CacheItem> cacheItems)
        {
            var firstItem = cacheItems.Last();

            return cacheItems.TryRemove(firstItem);
        }
    }
}
