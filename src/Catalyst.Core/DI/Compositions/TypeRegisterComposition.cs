namespace Catalyst.Core.DI.Compositions
{
    using Catalyst.Core.Registers;

    using LightInject;

    /// <summary>
    /// Adds type registry service mappings to the container
    /// </summary>
    internal class TypeRegisterComposition : ICompositionRoot
    {
        /// <inheritdoc />
        public void Compose(IServiceRegistry container)
        {
            container.RegisterSingleton<IMappingConfigurationRegister, DbMappingRegister>();

            container.RegisterSingleton<IValueConverterRegister, ValueConverterRegister>();
        }
    }
}