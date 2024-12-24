using Cache.Common;
using CustomFileCach;

ICache cache = new FileCach(new()
{
    TimeProvider= TimeProvider.System,
    DefaultExpiry= TimeSpan.FromSeconds(10),
});

//cache.Set("fullName", "Zekeriya sarıca");
//cache.Set("fullName", "Zekeriya sarıca_newValue");

//bool contains = cache.Contains("fullName");

var readValue = cache.Get("fullName");

Console.WriteLine("Value = {0}", readValue.Value);
Console.ReadLine();