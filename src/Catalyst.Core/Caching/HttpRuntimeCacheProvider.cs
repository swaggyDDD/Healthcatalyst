namespace Catalyst.Core.Caching
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Caching;

    using Catalyst.Core.DI;

    /// <summary>
    /// Represents a cache provider for HttpRuntime caching.
    /// </summary>
    public class HttpRuntimeCacheProvider : CacheProviderBase, IRuntimeCacheProvider
    {
        /// <summary>
        /// The <see cref="Cache"/>.
        /// </summary>
        private readonly Cache _cache;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpRuntimeCacheProvider"/> class.
        /// </summary>
        /// <param name="cache">
        /// The cache.
        /// </param>
        public HttpRuntimeCacheProvider(Cache cache)
        {
            _cache = cache;
        }

        /// <inheritdoc />
        public void ClearAllCache()
        {
            foreach (var entry in GetDictionaryEntries().ToArray())
            {
                _cache.Remove((string)entry.Key);
            }
        }

        /// <inheritdoc />
        public void ClearCacheItem(string cacheKey)
        {
            _cache.Remove(GetCacheKey(cacheKey));
        }

        /// <inheritdoc />
        public object GetCacheItem(string cacheKey)
        {
            return _cache.Get(GetCacheKey(cacheKey));
        }

        /// <inheritdoc />
        public object GetCacheItem(string cacheKey, Func<object> getCacheItem)
        {
            // Cache for 5 minutes
            return GetCacheItem(cacheKey, getCacheItem, TimeSpan.FromMinutes(5));
        }

        /// <inheritdoc />
        public object GetCacheItem(
            string cacheKey,
            Func<object> getCacheItem,
            TimeSpan? timeout,
            bool isSliding = false,
            CacheItemPriority priority = CacheItemPriority.Normal)
        {
            var result = GetCacheItem(cacheKey);
            if (result != null) return result;

            try
            {
                // TODO Fixme
                if (timeout == null) timeout = TimeSpan.FromMinutes(1);

                result = getCacheItem();
                var absolute = isSliding ? System.Web.Caching.Cache.NoAbsoluteExpiration : (timeout == null ? System.Web.Caching.Cache.NoAbsoluteExpiration : DateTime.Now.Add(timeout.Value));
                var sliding = isSliding == false ? System.Web.Caching.Cache.NoSlidingExpiration : (timeout ?? System.Web.Caching.Cache.NoSlidingExpiration);

                _cache.Insert(GetCacheKey(cacheKey), result, null, absolute, sliding, priority, null);

                return result;
            }
            catch (Exception ex)
            {
                Active.Logger.WarnWithException<HttpRuntimeCacheProvider>("Failed to invoke cache item getter.", ex);
                return null;
            }
        }

        /// <inheritdoc />
        public void InsertCacheItem(
            string cacheKey,
            Func<object> getCacheItem,
            TimeSpan? timeout = null,
            bool isSliding = false,
            CacheItemPriority priority = CacheItemPriority.Normal)
        {
            try
            {
                var result = getCacheItem();

                // don't cache nulls
                if (result != null)
                {
                    var absolute = isSliding ? System.Web.Caching.Cache.NoAbsoluteExpiration : (timeout == null ? System.Web.Caching.Cache.NoAbsoluteExpiration : DateTime.Now.Add(timeout.Value));
                    var sliding = isSliding == false ? System.Web.Caching.Cache.NoSlidingExpiration : (timeout ?? System.Web.Caching.Cache.NoSlidingExpiration);

                    _cache.Insert(GetCacheKey(cacheKey), result, null, absolute, sliding, priority, null);
                }
            }
            catch (Exception ex)
            {
                Active.Logger.WarnWithException<HttpRuntimeCacheProvider>("Failed to invoke cache item getter.", ex);
            }
        }

        /// <summary>
        /// Gets the cached items.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        protected virtual IEnumerable<DictionaryEntry> GetDictionaryEntries()
        {
            const string Prefix = CacheItemPrefix + "-";
            return _cache.Cast<DictionaryEntry>()
                .Where(x => x.Key is string && ((string)x.Key).StartsWith(Prefix));
        }
    }
}