﻿namespace Catalyst.Core.Caching
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Caching;

    /// <summary>
    /// Represents a cache provider that does not cache anything.
    /// </summary>
    public class NullCacheProvider : IRuntimeCacheProvider
    {
        /// <inheritdoc />
        public virtual void ClearAllCache()
        {
        }

        /// <inheritdoc />
        public virtual void ClearCacheItem(string key)
        {
        }

        /// <inheritdoc />
        public virtual void ClearCacheObjectTypes(string typeName)
        {
        }

        /// <inheritdoc />
        public virtual void ClearCacheObjectTypes<T>()
        {
        }

        /// <inheritdoc />
        public virtual void ClearCacheObjectTypes<T>(Func<string, T, bool> predicate)
        {
        }

        /// <inheritdoc />
        public virtual void ClearCacheByKeySearch(string keyStartsWith)
        {
        }

        /// <inheritdoc />
        public virtual void ClearCacheByKeyExpression(string regexString)
        {
        }

        /// <inheritdoc />
        public virtual IEnumerable<object> GetCacheItemsByKeySearch(string keyStartsWith)
        {
            return Enumerable.Empty<object>();
        }

        /// <inheritdoc />
        public IEnumerable<object> GetCacheItemsByKeyExpression(string regexString)
        {
            return Enumerable.Empty<object>();
        }

        /// <inheritdoc />
        public virtual object GetCacheItem(string cacheKey)
        {
            return default(object);
        }

        /// <inheritdoc />
        public virtual object GetCacheItem(string cacheKey, Func<object> getCacheItem)
        {
            return getCacheItem();
        }

        /// <inheritdoc />
        public object GetCacheItem(string cacheKey, Func<object> getCacheItem, TimeSpan? timeout, bool isSliding = false, CacheItemPriority priority = CacheItemPriority.Normal)
        {
            return getCacheItem();
        }

        /// <inheritdoc />
        public void InsertCacheItem(
            string cacheKey,
            Func<object> getCacheItem,
            TimeSpan? timeout = null,
            bool isSliding = false,
            CacheItemPriority priority = CacheItemPriority.Normal)
        {
        }
    }
}