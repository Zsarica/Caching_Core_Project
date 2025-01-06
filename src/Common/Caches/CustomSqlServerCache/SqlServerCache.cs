using Cache.Common;
using System.Collections.ObjectModel;

namespace CustomSqlServerCache
{
    public class SqlServerCache : ICache
    {
        public bool Contains(string key)
        {
            throw new NotImplementedException();
        }

        public CacheItem Get(string key)
        {
            throw new NotImplementedException();
        }

        public ReadOnlyDictionary<string, CacheItem> GetItems()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetKeys()
        {
            throw new NotImplementedException();
        }

        public T GetValue<T>(string key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CacheItem> GetValues()
        {
            throw new NotImplementedException();
        }

        public bool IsExpired(string key)
        {
            throw new NotImplementedException();
        }

        public bool Remove(string key)
        {
            throw new NotImplementedException();
        }

        public bool RemoveIfExpired(string key)
        {
            throw new NotImplementedException();
        }

        public void Set(string key, object value, TimeSpan? expiry = null)
        {
            throw new NotImplementedException();
        }

        public bool TryGet(string key, out CacheItem cachItem)
        {
            throw new NotImplementedException();
        }
    }
}
