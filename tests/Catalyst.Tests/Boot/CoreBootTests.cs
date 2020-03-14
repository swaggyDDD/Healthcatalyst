namespace Catalyst.Tests.Boot
{
    using Catalyst.Core.Boot;
    using Catalyst.Core.DI;

    using LightInject;

    using Moq;

    using NUnit.Framework;

    public class CoreBootTests
    {
        /// <summary>
        /// Tests the CoreBoot static event to ensure it's called
        /// </summary>
        [Test]
        public void CompleteEvent()
        {
            //// Arrange
            var completeCalled = false;
            CoreBoot.Complete += (s, e) => { completeCalled = true; };

            var container = new Mock<IServiceContainer>();
            var loader = new CoreBoot(container.Object);

            //// Act
            loader.Boot();


            //// Assert
            Assert.That(completeCalled, Is.True);
        }

        [Test]
        public void ActiveHasContainer()
        {
            //// Arrange
            var container = new Mock<IServiceContainer>();
            var loader = new CoreBoot(container.Object);

            //// Act
            loader.Boot();

            //// Assert
            Assert.That(Active.Container, Is.Not.Null);
        }
    }
}