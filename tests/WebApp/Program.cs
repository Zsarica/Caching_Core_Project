using Cache.Common;
using CustomFileCach.Extensions;
using CustomMemeoryCache;
using CustomMemeoryCache.Extensions;
using Microsoft.AspNetCore.Mvc;
using CustomSqlServerCache.Application.Extensions;
using CustomSqlServerCache.Infrastructure.DbContexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
/*
builder.Services.AddCustomFileCache(new()
{
    TimeProvider = TimeProvider.System,
    DefaultExpiry = TimeSpan.FromSeconds(10),
});

builder.Services.AddCustomMemoryCache(new MemoryCacheOptionsBuilder()
                                        .WithTimeProvider(TimeProvider.System)
                                        .WithDefaultExpiry(TimeSpan.FromSeconds(10))
                                        .WithCapacity(4, CacheEvictionPolicy.LeastFrequentlyUsed)
                                        .Build());
*/
builder.Services.AddSqlServerCache(new SqlServerOptions()
{
    ConnectionString = "Data Source=DESKTOP-AUNU480\\SQLEXPRESS;Initial Catalog=CacheDb;Integrated Security=True;Encrypt=False;Trust Server Certificate=True",
    TimeProvider = TimeProvider.System,
    DefaultExpiry = TimeSpan.FromSeconds(5)
}); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/cache/set/{key}/{value}", ([FromServices] ICache cache, string key, string value) =>
{
    cache.Set(key, value); ;
    return "OK";
});

app.MapGet("/cache/get/{key}", ([FromServices] ICache cache, string key) =>
{
    var value = cache.Get(key); 
    return value;
});
app.MapGet("/cache/getAll", ([FromServices] ICache cache) =>
{
    var value = cache.GetItems();
    return value;
});


app.Run();
