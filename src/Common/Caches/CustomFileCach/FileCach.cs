using Cache.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CustomFileCach
{
    public class FileCach : ICache
    {
        private static readonly JsonSerializerOptions opt = new() { WriteIndented = true };
        private readonly string FileDir = Path.Combine(Directory.GetCurrentDirectory(), "cache_files");
        private const string FileExtension = ".json";
        private const string SearchPattern = $"*{FileExtension}";
        private const string SearchPatternFormatKey = $"{{0}}{FileExtension}";
        private readonly FileCacheOptions fileCacheOptions;

        public FileCach(FileCacheOptions fileCacheOptions)
        {
            this.fileCacheOptions = fileCacheOptions;
        }

        public bool Contains(string key)
        {
            var pattern = string.Format(SearchPatternFormatKey, key);
            var files = Directory.GetFiles(FileDir,searchPattern: pattern);

            return files.Length > 0;
        }

        public CacheItem Get(string key)
        {
            var filepath = GenerateFileName(key);
            if(!File.Exists(filepath))
                return null;

            using var fs = new FileStream(filepath, FileMode.Open); 

            var cacheItem = JsonSerializer.Deserialize<CacheItem>(fs);
            return cacheItem;
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

        public bool RemoveIsExpired(string key)
        {
            throw new NotImplementedException();
        }

        public void Set(string key, object value, TimeSpan? expiry = null)
        {
            var jsonValue = value is not string
                ? JsonSerializer.Serialize(value, opt) : value.ToString();

            var cacheItem = new CacheItem(key, jsonValue, GetExpiryDate(expiry));
            var filePath = GenerateFileName(key);
            var directory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            File.WriteAllText(filePath, JsonSerializer.Serialize(cacheItem, opt));
        }

        public bool TryGet(string key, out CacheItem cachItem)
        {
            throw new NotImplementedException();
        }

        private DateTime? GetExpiryDate(TimeSpan? expiry)
        {
            TimeSpan? expiryDate = expiry ?? fileCacheOptions.DefaultExpiry;

            return expiryDate.HasValue ? fileCacheOptions.TimeProvider.GetLocalNow().Add(expiryDate.Value).LocalDateTime : default;
        }
        private string GenerateFileName(string key)
        {
            var fileName = $"{key}{FileExtension}";

            return Path.Combine(FileDir,fileName) ;
        }
    }
}
