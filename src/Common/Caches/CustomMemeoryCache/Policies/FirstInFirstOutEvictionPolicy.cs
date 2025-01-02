using Cache.Common;
using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;

namespace CustomMemeoryCache.Policies
{
    public class FirstInFirstOutEvictionPolicy : IEvictionPolicy
    {
        private readonly List<string> items = new();
        public bool Execute(ConcurrentDictionary<string, CacheItem> cacheItems)
        {
            var firstItemKey = items.FirstOrDefault();
            var cacheItem = cacheItems[firstItemKey];
            items.Remove(firstItemKey);
            return cacheItems.TryRemove(new KeyValuePair<string, CacheItem>(firstItemKey,cacheItem));
            
        }

        public void OnItemAccessed(string key)
        {
            
        }

        public void onItemAdded(string key)
        {
            items.Add(key);
        }

        public void OnItemRemoved(string key)
        {
            items.Remove(key);
        }
    }
}
