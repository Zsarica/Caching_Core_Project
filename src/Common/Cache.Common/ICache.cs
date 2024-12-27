using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cache.Common
{
    public interface ICache
    {
        IEnumerable<string> GetKeys();
        IEnumerable<CacheItem> GetValues();
        ReadOnlyDictionary<string, CacheItem> GetItems();
        void Set(string key, object value, TimeSpan? expiry = null);
        CacheItem Get(string key);
        bool TryGet(string key, out CacheItem cachItem);
        bool Contains(string key);
        T GetValue<T>(string key);
        bool IsExpired(string key);
        bool Remove(string key);
        bool RemoveIfExpired(string key);
        
        
       


    }
}
