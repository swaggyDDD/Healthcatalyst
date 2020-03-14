namespace Catalyst.Core.DI.Compositions
{
    using Catalyst.Core.Services;

    using LightInject;

    /// <summary>
    /// Adds "service" related service mappings to the container
    /// </summary>
    internal class ServiceComposition : ICompositionRoot
    {
        /// <inheritdoc />
        public void Compose(IServiceRegistry container)
        {
            container.RegisterSingleton<IPersonService, PersonService>();
            container.RegisterSingleton<IAddressService, AddressService>();
            container.RegisterSingleton<IExtendedPropertyService, ExtendedPropertyService>();

            // Register the service context
            container.RegisterSingleton<IServiceContext, ServiceContext>();
        }
    }
}