namespace Catalyst.Tests
{
    using Catalyst.Core.DI;
    using Catalyst.Tests.TestHelpers;

    using LightInject;

    using Merchello.Core;

    using NUnit.Framework;

    public class ActivatorProviderTests : CoreBootTestBase
    {
        [Test]
        public void CanResolveActivatorServiceProvider()
        {
            var provider = Active.Container.GetInstance<IActivatorServiceProvider>();

            Assert.That(provider, Is.Not.Null);
        }
    }
}