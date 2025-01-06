using Cache.Common;
using CustomSqlServerCache.Application.HostedServices;
using CustomSqlServerCache.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomSqlServerCache.Application.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddSqlServerCache(this IServiceCollection services, SqlServerOptions options)
        {
            ArgumentNullException.ThrowIfNull(options, nameof(options));
            ArgumentException.ThrowIfNullOrEmpty(options.ConnectionString, nameof(options.ConnectionString));

            services.AddSingleton(options);
            services.AddScoped<ICache, SqlServerCache>();
            services.AddDbContext<CacheDbContext>(opt =>
            {
#if DEBUG
                opt.LogTo(Console.WriteLine);
                opt.EnableSensitiveDataLogging();
#endif
                opt.UseSqlServer(options.ConnectionString, sqlOpt =>
                {
                    sqlOpt.EnableRetryOnFailure();
                });
            });

            services.AddHostedService<DbMigrationHostedService>();

            return services;
        }
    }
}
