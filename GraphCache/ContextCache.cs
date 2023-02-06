
using Microsoft.Extensions.Caching.Memory;
using Utils.Interfaces.Cache;

namespace GraphCache
{
    public class ContextCache<Dto> : ICache<Dto>
    {
        private readonly IMemoryCache _MemoryCache;
        private readonly string _Key;
        private readonly MemoryCacheEntryOptions _memoryCacheOptions;

        public ContextCache(IMemoryCache memoryCache, string key)
        {
            _Key = key;
            _MemoryCache = memoryCache;

            _memoryCacheOptions = new MemoryCacheEntryOptions()
            {
                AbsoluteExpiration = DateTime.Now.AddHours(24),
                Priority = CacheItemPriority.Normal,
                SlidingExpiration = TimeSpan.FromMinutes(5)
            };
        }

        public ContextCache(IMemoryCache memoryCache, string key, DateTime absoluteExpiration, CacheItemPriority cacheItemPriority, TimeSpan slidingExpiration)
        {
            _Key = key;
            _MemoryCache = memoryCache;

            _memoryCacheOptions = new MemoryCacheEntryOptions()
            {
                AbsoluteExpiration = absoluteExpiration,
                Priority = cacheItemPriority,
                SlidingExpiration = slidingExpiration
            };
        }

        public virtual List<Dto> Find()
        {
            return _MemoryCache.Get<List<Dto>>(_Key) ?? new List<Dto>();
        }

        public virtual List<Dto> Find(Predicate<Dto> predicate)
        {
            return Find().FindAll(predicate);
        }

        public virtual void Set(List<Dto> dtos)
        {
            List<Dto> currentCache = Find();
            currentCache.AddRange(dtos);

            _MemoryCache.Set(_Key, currentCache, _memoryCacheOptions);
        }

        public virtual void Set(Dto dto)
        {
            List<Dto> currentCache = Find();

            if (currentCache.Count > 0)
            {
                currentCache.Add(dto);
                _MemoryCache.Set(_Key, currentCache, _memoryCacheOptions);
            }
        }

        public virtual void Update(Dto dto, Predicate<Dto> predicate)
        {
            List<Dto> currentCache = Find();

            if (currentCache.Count > 0)
            {
                int index = currentCache.FindIndex(predicate);

                if (index >= 0)
                {
                    currentCache.RemoveAt(index);
                }

                currentCache.Add(dto);
                _MemoryCache.Set(_Key, currentCache, _memoryCacheOptions);
            }
        }

        public virtual void Remove(Predicate<Dto> predicate)
        {
            List<Dto> currentCache = Find();
            currentCache.RemoveAll(predicate);

            _MemoryCache.Set(_Key, currentCache, _memoryCacheOptions);
        }

        public virtual void Flush()
        {
            _MemoryCache.Remove(_Key);
        }

    }
}
