namespace Catalyst.Tests.Boot
{
    using Catalyst.Core.Boot;
    using Catalyst.Core.Caching;
    using Catalyst.Core.DI;
    using Catalyst.Web.Boot;

    using LightInject;

    using NUnit.Framework;

    [TestFixture]
    public class CacheSetupTests
    {
        [Test]
        public void CoreBootRequestCache()
        {
            //// Arrange
            var container = new ServiceContainer();

            var loader = new CoreBoot(container);

            //// Act
            loader.Boot();

            //// Assert
            Assert.That(Active.CacheManager, Is.Not.Null);
            Assert.That(Active.CacheManager.RequestCache, Is.Not.Null);
            Assert.That(Active.CacheManager.RequestCache.GetType(), Is.EqualTo(typeof(RequestCacheProvider)));
        }


        [Test]
        public void CoreBootHttpRuntimeCache()
        {
            //// Arrange
            var container = new ServiceContainer();

            var loader = new CoreBoot(container);

            //// Act
            loader.Boot();

            //// Assert
            Assert.That(Active.CacheManager, Is.Not.Null);
            Assert.That(Active.CacheManager.RuntimeCache, Is.Not.Null);
            Assert.That(Active.CacheManager.RuntimeCache.GetType(), Is.EqualTo(typeof(NullCacheProvider)));
        }


        /// <summary>
        /// Ensures the NullCacheProvider is replaced with the HttpRuntimeCacheProvider when using WebBoot
        /// </summary>
        [Test]
        public void WebBootHttpRuntimeCache()
        {
            //// Arrange
            var container = new ServiceContainer();

            var loader = new WebBoot(container);

            //// Act
            loader.Boot();

            //// Assert
            Assert.That(Active.CacheManager, Is.Not.Null);
            Assert.That(Active.CacheManager.RuntimeCache, Is.Not.Null);
            Assert.That(Active.CacheManager.RuntimeCache.GetType(), Is.EqualTo(typeof(HttpRuntimeCacheProvider)));
        }


        [OneTimeTearDown]
        public void Teardown()
        {
            if (Active.Container != null) Active.Container.Dispose();
        }
    }
}