namespace Catalyst.Tests.TestHelpers
{
    using System;

    using Catalyst.Core.Boot;
    using Catalyst.Core.DI;

    using LightInject;

    using NUnit.Framework;

    [TestFixture]
    public abstract class CoreBootTestBase
    {
        [OneTimeSetUp]
        public virtual void Initialize()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", TestHelper.TestAppDataDirectory);

            var container = TestHelper.GetEmptyServiceContainer();

            var loader = new CoreBoot(container);

            loader.Boot();
        }

        [OneTimeTearDown]
        public virtual void Teardown()
        {
            if (Active.Container != null) Active.Container.Dispose();
        }
    }
}