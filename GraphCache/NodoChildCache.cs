
using Microsoft.Extensions.Caching.Memory;

namespace GraphCache
{
    public class NodoChildCache<Dto> : ContextCache<Dto>
    {
        public NodoChildCache(IMemoryCache memoryCache) : base(memoryCache, "NodoChildCache", DateTime.Now.AddHours(1), CacheItemPriority.Normal, TimeSpan.FromMinutes(5))
        {

        }
    }
}
