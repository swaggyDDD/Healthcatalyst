namespace Catalyst.Core.Caching
{
    using System;
    using System.Web.Caching;

    /// <summary>
    /// Represents a runtime cache provider.
    /// </summary>
    public interface IRuntimeCacheProvider : ICacheProvider
    {
        /// <summary>
        /// Gets an object from cache or sets an object into cache if not found.
        /// </summary>
        /// <param name="cacheKey">
        /// The cache key.
        /// </param>
        /// <param name="getCacheItem">
        /// The get cache item.
        /// </param>
        /// <param name="timeout">
        /// The cache timeout.
        /// </param>
        /// <param name="isSliding">
        /// A value to indicate the cache dependency has a sliding expiration.
        /// </param>
        /// <param name="priority">
        /// The caching priority.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        object GetCacheItem(string cacheKey, Func<object> getCacheItem, TimeSpan? timeout, bool isSliding = false, CacheItemPriority priority = CacheItemPriority.Normal);

        /// <summary>
        /// Inserts an item into cache.
        /// </summary>
        /// <param name="cacheKey">
        /// The cache key.
        /// </param>
        /// <param name="getCacheItem">
        /// The get cache item.
        /// </param>
        /// <param name="timeout">
        /// The cache timeout.
        /// </param>
        /// <param name="isSliding">
        /// A value to indicate the cache dependency has a sliding expiration.
        /// </param>
        /// <param name="priority">
        /// The caching priority.
        /// </param>
        void InsertCacheItem(string cacheKey, Func<object> getCacheItem, TimeSpan? timeout = null, bool isSliding = false, CacheItemPriority priority = CacheItemPriority.Normal);
    }
}