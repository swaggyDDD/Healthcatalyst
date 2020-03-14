namespace Catalyst.Tests.Caching
{
    using System.Web;

    using Catalyst.Core.Caching;
    using Catalyst.Tests.TestHelpers.Fakes;

    using NUnit.Framework;

    [TestFixture]
    public class RequestCacheProviderTests
    {
        [Test]
        public void CanGetCacheWithoutHttpContext()
        {
            //// Arrange
            var provider = new RequestCacheProvider();

            //// Act
            var cache = provider.GetCache();

            //// Assert
            Assert.That(cache, Is.Not.Null);
        }

        [Test]
        public void CanGetCacheWithHttpContext()
        {
            //// Arrange
            HttpContext.Current = FakeHttpContext.Get();
            var provider = new RequestCacheProvider();

            //// Act
            var cache = provider.GetCache();

            //// Assert
            Assert.That(cache, Is.Not.Null);
        }


        [Test]
        public void GetCachedItem()
        {
            //// Arrange
            HttpContext.Current = FakeHttpContext.Get();
            var provider = new RequestCacheProvider();

            var key = "key";
            var value = "test";

            //// Act / Assert
            var result = provider.GetCacheItem(key);

            Assert.That(result, Is.Null, "Result was not initially null");

            result = provider.GetCacheItem(key, () => value);

            Assert.That(result, Is.EqualTo(value), "Result was not equal to value after insert");

            var freshy = provider.GetCacheItem(key);
            Assert.That(freshy, Is.Not.Null, "Freshy was null");
            Assert.That(freshy, Is.EqualTo(value));

        }


        [TearDown]
        public void Teardown()
        {
            HttpContext.Current = null;   
        }
    }
}