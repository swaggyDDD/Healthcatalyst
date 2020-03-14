namespace Catalyst.Core
{
    using System;
    using System.Linq;
    using System.Reflection;

    using Catalyst.Core.Logging;

    using Merchello.Core;

    /// <summary>
    /// Represents a provider that instantiates services.
    /// </summary>
    internal class ActivatorServiceProvider : IActivatorServiceProvider
    {
        /// <summary>
        /// The <see cref="ILogger"/>.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivatorServiceProvider"/> class.
        /// </summary>
        /// <param name="logger">
        /// The logger.
        /// </param>
        public ActivatorServiceProvider(ILogger logger)
        {
            if (logger == null) throw new ArgumentNullException(nameof(logger));

            _logger = logger;
        }

        /// <summary>
        /// Gets an instance of a service.
        /// </summary>
        /// <param name="serviceType">
        /// The type of the service.
        /// </param>
        /// <returns>
        /// The instantiated service.
        /// </returns>
        public object GetService(Type serviceType)
        {
            try
            {
                return Activator.CreateInstance(serviceType);
            }
            catch (Exception ex)
            {
                _logger.Error<ActivatorServiceProvider>($"Failed to instantiate type {serviceType.Name}", ex);
                throw;
            }
        }

        /// <summary>
        /// Gets an instance of the service.
        /// </summary>
        /// <param name="constructorArgumentValues">
        /// The constructor argument values.
        /// </param>
        /// <typeparam name="TService">
        /// The type of the service
        /// </typeparam>
        /// <returns>
        /// The <see cref="TService"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// Throws an exception if the instance of TService fails to instantiate.
        /// </exception>
        public TService GetService<TService>(object[] constructorArgumentValues) where TService : class
        {
            var type = typeof(TService);
            return this.GetService<TService>(type, constructorArgumentValues);
        }

        /// <summary>
        /// Gets an instance of the service.
        /// </summary>
        /// <param name="type">
        /// The actual type.
        /// </param>
        /// <param name="ctrArgs">
        /// The constructor argument values.
        /// </param>
        /// <typeparam name="TService">
        /// The type of the service
        /// </typeparam>
        /// <returns>
        /// The <see cref="TService"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// Throws an exception if the instance of TService fails to instantiate.
        /// </exception>
        public TService GetService<TService>(Type type, object[] ctrArgs) where TService : class
        {
            const System.Reflection.BindingFlags BindingFlags = System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | BindingFlags.NonPublic;

            var constructorArgumentTypes = ctrArgs.Select(value => value.GetType()).ToList();

            var constructor = type.GetConstructor(BindingFlags, null, CallingConventions.Any, constructorArgumentTypes.ToArray(), null);

            try
            {
                var obj = constructor.Invoke(ctrArgs);

                if (obj is TService)
                {
                    return obj as TService;
                }

                var invalidCast = new InvalidCastException($"Could not cast to {obj.GetType().FullName} to {typeof(TService).FullName}");
                _logger.Error<ActivatorServiceProvider>($"Failed to cast {type.Name}", invalidCast);
                throw invalidCast;
            }
            catch (Exception ex)
            {
                _logger.Error<ActivatorServiceProvider>($"Failed to instantiate instance", ex);
                throw;
            }
        }
    }
}