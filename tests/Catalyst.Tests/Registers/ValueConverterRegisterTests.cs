namespace Catalyst.Tests.Registers
{
    using System;
    using System.Linq;

    using Catalyst.Core;
    using Catalyst.Core.Logging;
    using Catalyst.Core.Models.Domain;
    using Catalyst.Core.Registers;
    using Catalyst.Core.ValueConverters;
    using Catalyst.Tests.TestHelpers;

    using Merchello.Core;

    using NUnit.Framework;

    public class ValueConverterRegisterTests
    {
        private IValueConverterRegister register;

        [OneTimeSetUp]
        public void Initialize()
        {

            var container = TestHelper.GetEmptyServiceContainer();
            container.RegisterSingleton<ILogger>(factory => Logger.CreateWithDefaultLog4NetConfiguration());
            container.RegisterSingleton<IActivatorServiceProvider, ActivatorServiceProvider>();
            register = new ValueConverterRegister(container, Logger.CreateWithDefaultLog4NetConfiguration());

        }

        [Test]
        public void InstanceTypes()
        {
            //// Arrange
            const int expected = 3;
            
            //// Act
            var types = register.InstanceTypes;

            //// Assert
            Assert.That(types.Count(), Is.EqualTo(expected));
        }

        [TestCase(typeof(PhotoValueConverter))]
        [TestCase(typeof(SocialLinksValueConverter))]
        [TestCase(typeof(InterestListValueConverter))]
        [Test]
        public void InstanceTypesContain(Type type)
        {
            Assert.That(register.InstanceTypes.Contains(type), Is.True);
        }

        [TestCase(Constants.ExtendedProperties.PhotoConverterAlias)]
        [TestCase(Constants.ExtendedProperties.SocialLinksConverterAlias)]
        [TestCase(Constants.ExtendedProperties.InterestListConverterAlias)]
        [Test]
        public void ConverterMappings(string alias)
        {
            Assert.That(register.ConverterMappings.Any(x => x.ConverterAlias.Equals(alias)), Is.True);
        }

        [TestCase(Constants.ExtendedProperties.PhotoConverterAlias, typeof(PhotoValueConverter))]
        [TestCase(Constants.ExtendedProperties.SocialLinksConverterAlias, typeof(SocialLinksValueConverter))]
        [TestCase(Constants.ExtendedProperties.InterestListConverterAlias, typeof(InterestListValueConverter))]
        [Test]
        public void GetInstanceFor(string alias, Type expected)
        {
            //// Arrange
            var fake = new ExtendedProperty { ConverterAlias = alias, Value = string.Empty };

            //// Act
            var converter = register.GetInstanceFor(fake);

            //// Assert
            Assert.That(converter.GetType(), Is.EqualTo(expected));
        }
    }
}