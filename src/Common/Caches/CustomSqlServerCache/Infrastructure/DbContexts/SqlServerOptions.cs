namespace CustomSqlServerCache.Infrastructure.DbContexts
{
    public class SqlServerOptions
    {
        public TimeProvider TimeProvider { get; set; }
        public TimeSpan? DefaultExpiry { get; set; }
        public required string ConnectionString { get; set; }
    }
}
