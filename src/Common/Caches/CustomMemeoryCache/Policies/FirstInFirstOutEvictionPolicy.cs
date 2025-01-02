using Cache.Common;
using System.Collections.Concurrent;

namespace CustomMemeoryCache.Policies
{
    public class FirstInFirstOutEvictionPolicy : IEvictionPolicy
    {
        public bool Execute(ConcurrentDictionary<string, CacheItem> cacheItems)
        {
            var firstItem = cacheItems.First();

            return cacheItems.TryRemove(firstItem);
        }
    }
}
