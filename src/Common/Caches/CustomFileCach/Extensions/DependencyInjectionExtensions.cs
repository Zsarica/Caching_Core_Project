using Cache.Common;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomFileCach.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddCustomFileCache(this IServiceCollection services,
            FileCacheOptions options)
        {
            services.AddSingleton(options);
            services.AddSingleton<ICache, FileCach>();

            return services;
            
        }
    }
}
