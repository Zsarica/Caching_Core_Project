using Cache.Common;
using System.Collections.Concurrent;

namespace CustomMemeoryCache.Policies
{
    public class NullEvictionPolicy : IEvictionPolicy
    {
        public bool Execute(ConcurrentDictionary<string, CacheItem> cacheItems)
        {
            

            return true;
        }
    }
}
