using Microsoft.Extensions.Caching.Memory;

namespace GraphCache
{
    public class NodoFatherCache<Dto> : ContextCache<Dto>
    {

        public NodoFatherCache(IMemoryCache memoryCache) : base(memoryCache, "NodoFatherCache", DateTime.Now.AddHours(1), CacheItemPriority.Normal, TimeSpan.FromMinutes(5))
        {

        }

    }
}
