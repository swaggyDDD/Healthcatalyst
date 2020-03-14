namespace Catalyst.Tests.Registers
{
    using System.Linq;

    using Catalyst.Core.Logging;
    using Catalyst.Core.Registers;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class DbMappingRegisterTests
    {
        [Test]
        public void InstanceTypes()
        {
            //// Arrange
            const int expectedCount = 3;
            var logger = new Mock<ILogger>().Object;

            //// Act
            var register = new DbMappingRegister(logger);

            //// Assert
            Assert.IsTrue(register.InstanceTypes.Any(), "No instance types found");
            Assert.That(register.InstanceTypes.Count(), Is.EqualTo(expectedCount), "Instance type count does not match");
        }

        [Test]
        public void GetInstantiations()
        {
            //// Arrange
            const int expectedCount = 3;
            var logger = new Mock<ILogger>().Object;

            //// Act
            var register = new DbMappingRegister(logger);
            var instances = register.GetInstantiations().ToArray();

            Assert.IsTrue(instances.Any());
            Assert.That(instances.Count(), Is.EqualTo(expectedCount));
        }
    }
}