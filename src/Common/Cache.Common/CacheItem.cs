using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cache.Common
{
    public class CacheItem
    {
        public CacheItem()
        {
            
        }

        public CacheItem(string key, object value)
        {
            Key = key;
            Value = value;
        }

        public CacheItem(string key, object value, DateTime? expiryDate)
        {
            Key = key;
            Value = value;
            ExpiryDate = expiryDate;
        }

        public string Key { get; set; }
        public object Value { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public T GetValue<T>() => (T)Value; 
    }
}
