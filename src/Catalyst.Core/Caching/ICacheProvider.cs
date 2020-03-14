namespace Catalyst.Core.Caching
{
    using System;

    /// <summary>
    /// Represents a Cache Provider.
    /// </summary>
    public interface ICacheProvider
    {
        /// <summary>
        /// Removes all items from the cache.
        /// </summary>
        void ClearAllCache();

        /// <summary>
        /// Removes an item from the cache, identified by its key.
        /// </summary>
        /// <param name="key">The key of the item.</param>
        void ClearCacheItem(string key);

        /// <summary>
        /// Gets an item from the cache with a given key
        /// </summary>
        /// <param name="cacheKey">The cache key</param>
        /// <returns>The cached object or null</returns>
        object GetCacheItem(string cacheKey);

        /// <summary>
        /// Attempts to get an item from cache with a given key. If the item is not found in cache, the object is retrieved
        /// via a function, cached and then returned.
        /// </summary>
        /// <param name="cacheKey">
        /// The cache key.
        /// </param>
        /// <param name="getCacheItem">
        /// The function to get the cached item.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        object GetCacheItem(string cacheKey, Func<object> getCacheItem);
    }
}