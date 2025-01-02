using Cache.Common;
using System.Collections.Concurrent;

namespace CustomMemeoryCache.Policies
{
    public class LFUEvictionPolicy : IEvictionPolicy
    {
        private readonly ConcurrentDictionary<string, int> accessCount = [];

        public bool Execute(ConcurrentDictionary<string, CacheItem> cacheItems)
        {
            if(cacheItems.IsEmpty || accessCount.IsEmpty)
            {
                return false;
            }
            var key = accessCount.OrderBy(i=>i.Value).FirstOrDefault().Key;
            if(key is not null )
            {
                cacheItems.TryRemove(key, out _);
                 return accessCount.TryRemove(key, out _);
            }

            return false;
        }

        public void OnItemAccessed(string key)
        {
            accessCount.AddOrUpdate(key, 1,(_,count) => count+1);
        }

        public void onItemAdded(string key)
        {
            accessCount[key] = 0;
        }

        public void OnItemRemoved(string key)
        {
            accessCount.TryRemove(key , out _ ); 
        }
    }
}
