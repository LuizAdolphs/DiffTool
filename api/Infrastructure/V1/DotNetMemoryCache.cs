namespace DiffProject.Infrastructure.V1
{
	using System;
	using System.Threading.Tasks;
	using api.Models.V1;
    using Microsoft.Extensions.Caching.Memory;

    public class DotNetMemoryCache : ICache
    {
        IMemoryCache _cache;
        MemoryCacheEntryOptions _cacheEntryOptions;

        public DotNetMemoryCache(IMemoryCache cache)
        {
            this._cache = cache;
            this._cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromHours(1));
        }

        public async Task AddAsync(string key, Data data)
        {
            await Task.Run(() => 
            {
                this._cache.Remove(key);
                this._cache.Set(key, data, this._cacheEntryOptions); 
            });
        }

        public async Task<Data?> GetAsync(string id)
        {
            return await Task.Run(() =>
            {
                this._cache.TryGetValue(id, out Data? data);

                return data;
            });
        }
    }
}
