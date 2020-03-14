namespace Catalyst.Core.Caching
{
    using System;

    /// <inheritdoc />
    internal class CacheManager : ICacheManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CacheManager"/> class.
        /// </summary>
        /// <param name="runtimeCache">
        /// The <see cref="IRuntimeCacheProvider"/>.
        /// </param>
        /// <param name="requestCache">
        /// The <see cref="ICacheProvider"/> for request based caching.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Throws if runtimeCache or requestCache is null.
        /// </exception>
        public CacheManager(IRuntimeCacheProvider runtimeCache, ICacheProvider requestCache)
        {
            if (runtimeCache == null) throw new ArgumentNullException(nameof(runtimeCache));
            if (requestCache == null) throw new ArgumentNullException(nameof(requestCache));

            RuntimeCache = runtimeCache;
            RequestCache = requestCache;
        }

        /// <inheritdoc />
        public IRuntimeCacheProvider RuntimeCache { get; }

        /// <inheritdoc />
        public ICacheProvider RequestCache { get; }
    }
}