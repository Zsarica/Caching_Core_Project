using Cache.Common;
using CustomSqlServerCache.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace CustomSqlServerCache
{
    public class SqlServerCache(CacheDbContext _dbContext, SqlServerOptions options) : ICache
    {
        public bool Contains(string key)
        {
            return _dbContext.Cache.Any(x => x.Key == key);
        }

        public CacheItem Get(string key)
        {
            var item = _dbContext.Cache.FirstOrDefault(x => x.Key == key);
            return item?.CacheItem;
        }

        public ReadOnlyDictionary<string, CacheItem> GetItems()
        {
            return _dbContext.Cache.ToDictionary(k => k.Key, k => k.CacheItem).AsReadOnly();
        }

        public IEnumerable<string> GetKeys()
        {
            return _dbContext.Cache.Select(i => i.Key);
        }

        public T GetValue<T>(string key)
        {
            var cacheItem = Get(key);
            return cacheItem is null ? default : cacheItem.GetValue<T>();
        }

        public IEnumerable<CacheItem> GetValues()
        {
            return _dbContext.Cache.Select(i => i.CacheItem);
        }

        public bool IsExpired(string key)
        {
            return _dbContext.Cache.Where(i => i.Key == key).
                Where(i => i.CacheItem.ExpiryDate != null &&
                i.CacheItem.ExpiryDate > options.TimeProvider.GetLocalNow().LocalDateTime).Any();
        }

        public bool Remove(string key)
        {
            var deletedRows = _dbContext.Cache
                .Where(i => i.Key == key)
                .ExecuteDelete();

            return deletedRows > 0;

        }

        public bool RemoveIfExpired(string key)
        {
            var deletedRows =  _dbContext.Cache.Where(i => i.Key == key).
               Where(i => i.CacheItem.ExpiryDate != null &&
               i.CacheItem.ExpiryDate > options.TimeProvider.GetLocalNow().LocalDateTime).ExecuteDelete();

            return deletedRows > 0;
        }

        public void Set(string key, object value, TimeSpan? expiry = null)
        {
            var Item = _dbContext.Cache.Find(key);
            if (Item is null)
            {
                var expiryDate = GetExpiryDate(expiry);
                Item = new Domain.Entities.CacheEntity()
                {
                    Key = key,
                    CacheItem = new CacheItem(key, value,expiryDate)
                };
                _dbContext.Add(Item);
                
            }
            else
            {
                TimeSpan? expiryDate =  expiry ?? options.DefaultExpiry;
                Item.CacheItem.Value = value.ToString();
                Item.CacheItem.ExpiryDate = expiryDate.HasValue ? 
                    options.TimeProvider.GetLocalNow().Add(expiryDate.Value).LocalDateTime : default;
            }
            _dbContext.SaveChanges();
        }

        public bool TryGet(string key, out CacheItem cachItem)
        {
            cachItem = Get(key);
            return cachItem is not null;
        }

        private DateTime? GetExpiryDate(TimeSpan? expiry)
        {
            TimeSpan? expiryDate = expiry ?? options.DefaultExpiry;

            return expiryDate.HasValue ? options.TimeProvider.GetLocalNow().Add(expiryDate.Value).LocalDateTime : default;
        }
    }
}
