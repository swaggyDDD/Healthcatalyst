namespace Catalyst.Core
{
    using System;
    using System.Linq;

    using LightInject;

    /// <summary>
    /// Extension methods for the Light Inject Service container.
    /// </summary>
    /// <seealso cref="https://github.com/umbraco/Umbraco-CMS/blob/dev-v8/src/Umbraco.Core/DI/LightInjectExtensions.cs"/>
    public static partial class Extensions
    {
        /// <summary>
        /// Registers the TService with the TImplementation as a singleton.
        /// </summary>
        /// <typeparam name="TService">
        /// The type of the service
        /// </typeparam>
        /// <typeparam name="TImplementation">
        /// The type of the instance
        /// </typeparam>
        /// <param name="container">
        /// The container.
        /// </param>
        internal static void RegisterSingleton<TService, TImplementation>(this IServiceRegistry container)
            where TImplementation : TService
        {
            var registration = container.GetAvailableService<TService>();

            if (registration == null)
                container.Register<TService, TImplementation>(new PerContainerLifetime());
            else
                container.UpdateRegistration(registration, typeof(TImplementation), null);
        }

        /// <summary>
        /// Registers the TService with the factory that describes the dependencies of the service, as a singleton.
        /// </summary>
        /// <typeparam name="TService">
        /// The type of the service instance.
        /// </typeparam>
        /// <param name="container">
        /// The container.
        /// </param>
        /// <param name="factory">
        /// The factory.
        /// </param>
        /// <param name="serviceName">
        /// The service Name.
        /// </param>
        internal static void RegisterSingleton<TService>(this IServiceRegistry container, Func<IServiceFactory, TService> factory, string serviceName)
        {
            var registration = container.GetAvailableService<TService>(serviceName);
            if (registration == null)
                container.Register(factory, serviceName, new PerContainerLifetime());
            else
                container.UpdateRegistration(registration, null, factory);
        }

        /// <summary>
        /// Registers the TService with the factory that describes the dependencies of the service, as a singleton.
        /// </summary>
        /// <typeparam name="TService">
        /// The type of the service.
        /// </typeparam>
        /// <param name="container">
        /// The container.
        /// </param>
        /// <param name="factory">
        /// The factory.
        /// </param>
        internal static void RegisterSingleton<TService>(this IServiceRegistry container, Func<IServiceFactory, TService> factory)
        {
            container.Register(factory, new PerContainerLifetime());
        }

        /// <summary>
        /// Determines if a service has already been registered/available.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        /// <typeparam name="TService">
        /// The type of the service
        /// </typeparam>
        /// <returns>
        /// The <see cref="ServiceRegistration"/>.
        /// </returns>
        internal static ServiceRegistration GetAvailableService<TService>(this IServiceRegistry container)
        {
            var typeofTService = typeof(TService);
            return container.AvailableServices.SingleOrDefault(x => x.ServiceType == typeofTService);
        }

        /// <summary>
        /// Determines if a service has already been registered/available.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <typeparam name="TService">
        /// The type of the service
        /// </typeparam>
        /// <returns>
        /// The <see cref="ServiceRegistration"/>.
        /// </returns>
        internal static ServiceRegistration GetAvailableService<TService>(this IServiceRegistry container, string name)
        {
            var typeofTService = typeof(TService);
            return container.AvailableServices.SingleOrDefault(x => x.ServiceType == typeofTService && x.ServiceName == name);
        }


        /// <summary>
        /// Updates a service registration.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        /// <param name="registration">
        /// The registration.
        /// </param>
        /// <param name="implementingType">
        /// The implementing type.
        /// </param>
        /// <param name="factoryExpression">
        /// The factory expression.
        /// </param>
        /// <remarks>
        /// Used when overriding registrations made in .Core in .Web or tests.
        /// </remarks>
        private static void UpdateRegistration(this IServiceRegistry container, ServiceRegistration registration, Type implementingType, Delegate factoryExpression)
        {
            registration.ImplementingType = implementingType;
            registration.FactoryExpression = factoryExpression;
        }
    }
}
