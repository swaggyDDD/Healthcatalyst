namespace Catalyst.Core.DI.Compositions
{
    using Catalyst.Core.Caching;

    using LightInject;

    /// <summary>
    /// Adds caching related service mappings to the container
    /// </summary>
    internal class CacheComposition : ICompositionRoot
    {
        /// <inheritdoc />
        public void Compose(IServiceRegistry container)
        {
            container.Register<ICacheProvider, RequestCacheProvider>();

            // This need to be overridden in .Web
            container.Register<IRuntimeCacheProvider, NullCacheProvider>();
            container.RegisterSingleton<ICacheManager, CacheManager>();
        }
    }
}