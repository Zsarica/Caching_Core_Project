using CustomMemeoryCache.Policies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMemeoryCache
{
    public class MemoryCacheOptions
    {
        internal IEvictionPolicy EvictionPolicy { get; set; }
        public int? Capacity { get; set; }
        public TimeProvider TimeProvider { get; set; } = TimeProvider.System;
        public TimeSpan? DefaultExpiry { get; set; }
        public StringComparer KeyComparer { get; set; } = StringComparer.OrdinalIgnoreCase;
    }

    public class MemoryCacheOptionsBuilder
    {
        private readonly MemoryCacheOptions options = new();

        public MemoryCacheOptionsBuilder WithTimeProvider(TimeProvider timeProvider)
        {
            options.TimeProvider = timeProvider;
            return this;
        }
        public MemoryCacheOptionsBuilder WithDefaultExpiry(TimeSpan? expiry)
        {
            options.DefaultExpiry = expiry;
            return this;
        }
        public MemoryCacheOptionsBuilder WithCapacity(int capacity, CacheEvictionPolicy policy)
        {

            options.Capacity = capacity;

            options.EvictionPolicy = policy switch
            {
                CacheEvictionPolicy.FirstInFirstOut => new FirstInFirstOutEvictionPolicy(),
                CacheEvictionPolicy.LastInFirstOut => new LastInFirstOutEvictionPolicy(),
                _ or CacheEvictionPolicy.None => new NullEvictionPolicy()
            };

            return this;
        }

        public MemoryCacheOptions Build() => options;

    }
}
