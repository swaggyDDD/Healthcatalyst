namespace Catalyst.Tests.Caching
{
    using System;
    using System.Web;
    using System.Web.Caching;

    using Catalyst.Core.Caching;
    using Catalyst.Tests.TestHelpers.Fakes;

    using NUnit.Framework;

    [TestFixture]
    public class HttpRuntimeCacheProviderTests
    {
        private IRuntimeCacheProvider runtimeCache;

        [OneTimeSetUp]
        public void Initialize()
        {
            const string prefix = "catalyst";


            // ReSharper disable once UseObjectOrCollectionInitializer
            var cache = HttpRuntime.Cache;
            cache.Add($"{prefix}.key1", "value1", null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
            cache.Add($"{prefix}.key2", "value2", null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
            cache.Add($"{prefix}.key3", "value3", null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
            cache.Add($"{prefix}.key4", "value4", null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);

            runtimeCache = new HttpRuntimeCacheProvider(cache);
        }

        [Test]
        public void GetCachedItem_ReturnsNull()
        { 
            //// Arrange
            var key = Guid.NewGuid().ToString();

            //// Act
            var result = runtimeCache.GetCacheItem(key);

            //// Assert
            Assert.That(result, Is.Null);

        }

        [TestCase("key1", "value1")]
        [TestCase("key2", "value2")]
        [TestCase("key3", "value3")]
        [TestCase("key4", "value4")]
        [Test]
        public void GetCachedItem_ReturnsValue(string key, string value)
        {
            //// Arrange
            // handled in setup

            //// Act
            var result = runtimeCache.GetCacheItem(key);

            //// Assert
            Assert.That(result.ToString(), Is.EqualTo(value));

        }

        [Test]
        public void GetCacheItem_CanBeSetWithFunc()
        {
            //// Arrange
            var key = Guid.NewGuid().ToString();  // random key
            var value = "test-value";

            //// Act / Assert
            var result = runtimeCache.GetCacheItem(key);
            
            Assert.That(result, Is.Null, "Result was not null!");

            result = runtimeCache.GetCacheItem(key, () => value);

            Assert.That(result, Is.Not.Null, "Result was null after function insert");

            var freshy = runtimeCache.GetCacheItem(key);

            Assert.That(freshy, Is.Not.Null, "Freshy was null");

        }


        [OneTimeTearDown]
        public void Teardown()
        {
            HttpContext.Current = null;
        }
    }
}