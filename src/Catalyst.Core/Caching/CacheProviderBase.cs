namespace Catalyst.Core.Caching
{
    /// <summary>
    /// Represents a base cache provider.
    /// </summary>
    public abstract class CacheProviderBase
    {
        /// <summary>
        /// A prefix for cache items.
        /// </summary>
        protected const string CacheItemPrefix = "catalyst";


        /// <summary>
        /// Gets the modified cache key (with the cache item prefix).
        /// </summary>
        /// <param name="cacheKey">
        /// The unmodified cache key.
        /// </param>
        /// <returns>
        /// The modified cache key.
        /// </returns>
        protected static string GetCacheKey(string cacheKey)
        {
            return $"{CacheItemPrefix}.{cacheKey}";
        }
    }
}