using Cache.Common;
using CustomFileCach;
using CustomMemeoryCache;

MemoryCacheOptions memoryCacheOptions = new MemoryCacheOptionsBuilder()
                                        .WithTimeProvider(TimeProvider.System)
                                        .WithDefaultExpiry(TimeSpan.FromSeconds(10))
                                        .WithCapacity(3,CacheEvictionPolicy.FirstInFirstOut)
                                        .Build();

ICache memoryCache = new MemoryCache(memoryCacheOptions);

memoryCache.Set("fullName", "Zekeriya sarıca");
memoryCache.Set("fullName2", "Zekeriya sarıca_newValue");

bool contains = memoryCache.Contains("fullName");

var readValue = memoryCache.Get("fullName2");

Console.WriteLine("Value = {0}", readValue.Value);
Console.ReadLine();


ICache cache = new FileCach(new()
{
    TimeProvider= TimeProvider.System,
    DefaultExpiry= TimeSpan.FromSeconds(10),
});

//cache.Set("fullName", "Zekeriya sarıca");
//cache.Set("fullName", "Zekeriya sarıca_newValue");

//bool contains = cache.Contains("fullName");

//var readValue = cache.Get("fullName");

