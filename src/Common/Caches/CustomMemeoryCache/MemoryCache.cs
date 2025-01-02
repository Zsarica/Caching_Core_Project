using Cache.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMemeoryCache
{
    public class MemoryCache : ICache
    {
        private readonly MemoryCacheOptions option;
        private readonly ConcurrentDictionary<string, CacheItem> cacheItems;

        public MemoryCache(MemoryCacheOptions option)
        {
            ArgumentNullException.ThrowIfNull(nameof(option));
            cacheItems = new(option.KeyComparer);
            this.option = option;
        }

        public bool Contains(string key) => cacheItems.ContainsKey(key);

        public CacheItem Get(string key)
        {
            if(cacheItems.TryGetValue(key, out var cacheItem))
            {
                return cacheItem;
            }
            return default;
        }

        public ReadOnlyDictionary<string, CacheItem> GetItems() => cacheItems.AsReadOnly();

        public IEnumerable<string> GetKeys() => cacheItems.Keys;
        

        public T GetValue<T>(string key)
        {
            var cacheItem = Get(key);
            return cacheItem == null ? default : cacheItem.GetValue<T>();
        }

        public IEnumerable<CacheItem> GetValues() => cacheItems.Values;

        public bool IsExpired(string key)
        {
            var cacheItem = Get(key) ?? throw new ArgumentException("cache item not found.");

            return cacheItem.ExpiryDate is not null &&
                cacheItem.ExpiryDate < option.TimeProvider.GetLocalNow();
        }

        public bool Remove(string key)
        {
            return cacheItems.TryRemove(key, out _);
        }

        public bool RemoveIfExpired(string key)
        {
            return IsExpired(key) && Remove(key);
        }

        public void Set(string key, object value, TimeSpan? expiry = null)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(key));

            EnsureCapacity();

            var expiryDate = GetExpiryDate(expiry);

            cacheItems[key] = new CacheItem(key, value,expiryDate);
        }

        public bool TryGet(string key, out CacheItem cachItem)
        {
            cachItem = Get(key);
            return cachItem is not null;

        }

        private DateTime? GetExpiryDate(TimeSpan? expiry)
        {
            TimeSpan? expiryDate = expiry ?? option.DefaultExpiry;

            return expiryDate.HasValue ? option.TimeProvider.GetLocalNow().Add(expiryDate.Value).LocalDateTime : default;
        }
        private void EnsureCapacity()
        {
            if(!option.Capacity.HasValue)
            {
                return;
            }

            if(cacheItems.Count >= option.Capacity.Value)
            {
                option.EvictionPolicy.Execute(cacheItems);
            }

        }
    }
}
