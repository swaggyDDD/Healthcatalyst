namespace Catalyst.Core.Caching
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Caching;
    using System.Web;

    using Catalyst.Core.DI;

    /// <summary>
    /// Represents a request based cache provider.
    /// </summary>
    internal class RequestCacheProvider : CacheProviderBase, ICacheProvider
    {
        /// <inheritdoc />
        public void ClearAllCache()
        {
            var cache = GetCache();
            var keys = cache.Keys;

            foreach (var key in keys)
            {
                if (key.ToString().StartsWith(CacheItemPrefix)) cache.Remove(key);
            }
        }

        /// <inheritdoc />
        public void ClearCacheItem(string key)
        {
            var cache = GetCache();
            cache.Remove(GetCacheKey(key));
        }

        /// <inheritdoc />
        public object GetCacheItem(string cacheKey)
        {
            var cache = GetCache();
            return cache[GetCacheKey(cacheKey)];
        }

        /// <inheritdoc />
        public object GetCacheItem(string cacheKey, Func<object> getCacheItem)
        {
            var cache = GetCache();
            var result = GetCacheItem(cacheKey);

            if (result != null) return result;

            try
            {
                result = getCacheItem();
                if (result != null)
                {
                    cache.Add(GetCacheKey(cacheKey), result);
                    return result;
                }

                return null;
            }
            catch (Exception ex)
            {
                Active.Logger.Warn<RequestCacheProvider>("Failed to retrieve item to cache from getter.");

                return null;
            }
        }

        /// <summary>
        /// Tries to get the HttpContext.Current.Items dictionary.
        /// </summary>
        /// <returns>
        /// A dictionary to use for caching.
        /// </returns>
        /// <remarks>
        /// Internal for caching
        /// </remarks>
        internal System.Collections.IDictionary GetCache()
        {
            return HttpContext.Current != null ? 
                HttpContext.Current.Items : 
                new System.Collections.Hashtable();
        }
    }
}