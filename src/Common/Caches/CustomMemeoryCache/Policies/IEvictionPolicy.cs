using Cache.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMemeoryCache.Policies
{
    public interface IEvictionPolicy
    {
        bool Execute(ConcurrentDictionary<string, CacheItem> cacheItems);

        void OnItemAccessed(string key);
        void OnItemRemoved(string key);
        void onItemAdded(string key);
    }
}
