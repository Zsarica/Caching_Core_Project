using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMemeoryCache
{
    public enum CacheEvictionPolicy
    {
        None = 0,
        LastInFirstOut = 1,
        FirstInFirstOut = 2,
        LeastFrequentlyUsed = 3
    }
}
