namespace Catalyst.Core.Caching
{
    /// <summary>
    /// Represents a cache manager.
    /// </summary>
    public interface ICacheManager
    {
        /// <summary>
        /// Gets the <see cref="ICacheProvider"/> for use with HttpRuntime caching.
        /// </summary>
        IRuntimeCacheProvider RuntimeCache { get; }

        /// <summary>
        /// Gets the <see cref="RequestCache"/> for use the request based caching.
        /// </summary>
        ICacheProvider RequestCache { get; }
    }
}