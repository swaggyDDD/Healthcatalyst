namespace Catalyst.Core.DI.Compositions
{
    using Catalyst.Core.Data.Context;
    using Catalyst.Core.Logging;
    using Catalyst.Core.Registers;

    using LightInject;

    /// <summary>
    /// Adds data related service mappings to the container
    /// </summary>
    internal class DataComposition : ICompositionRoot
    {
        /// <inheritdoc />
        public void Compose(IServiceRegistry container)
        {
            // Transient
            container.Register<CatalystDbContext>();

            container.Register<string, CatalystDbContext>(
                (factory, connStr) => new CatalystDbContext(
                    connStr,
                    factory.GetInstance<ILogger>(),
                    factory.GetInstance<IMappingConfigurationRegister>()));

            container.Register<CatalystDbContext>(
                factory =>
                    new CatalystDbContext(
                        Constants.Database.ConnectionStringName,
                        factory.GetInstance<ILogger>(),
                        factory.GetInstance<IMappingConfigurationRegister>()),
                Constants.Database.ConnectionStringName);
        }
    }
}