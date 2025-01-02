using Cache.Common;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMemeoryCache.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddCustomMemoryCache(this IServiceCollection services,
            MemoryCacheOptions options)
        {
            services.AddSingleton(options);
            services.AddSingleton<ICache, MemoryCache>();

            return services;
            
        }
    }
}
