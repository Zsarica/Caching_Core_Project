using CustomSqlServerCache.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomSqlServerCache.Application.HostedServices
{
    public class DbMigrationHostedService(IServiceScopeFactory serviceScopeFactory) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = serviceScopeFactory.CreateScope();
            CacheDbContext dbContext = scope.ServiceProvider.GetRequiredService<CacheDbContext>();

            await dbContext.Database.EnsureCreatedAsync(stoppingToken);
            await dbContext.Database.MigrateAsync(stoppingToken);

        }
    }
}
