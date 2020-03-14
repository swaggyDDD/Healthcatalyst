namespace Catalyst.Core.DI
{
    using System;
    using System.Collections.Generic;

    using Catalyst.Core.Caching;
    using Catalyst.Core.Data.Context;
    using Catalyst.Core.Logging;
    using Catalyst.Core.Registers;
    using Catalyst.Core.Services;
    using Catalyst.Core.ValueConverters;

    using LightInject;

    /// <summary>
    /// Exposes Catalyst the container.
    /// </summary>
    public static class Active
    {
        /// <summary>
        /// The <see cref="IServiceContainer"/>.
        /// </summary>
        private static IServiceContainer _container;

        /// <summary>
        /// The <see cref="ILogger"/>.
        /// </summary>
        public static ILogger Logger => Container.GetInstance<ILogger>();

        /// <summary>
        /// The <see cref="CacheManager"/>.
        /// </summary>
        public static ICacheManager CacheManager => Container.GetInstance<ICacheManager>();

        /// <summary>
        /// The <see cref="IServiceContext"/>.
        /// </summary>
        public static IServiceContext Services => Container.GetInstance<IServiceContext>();

        /// <summary>
        /// Gets the collection of <see cref="IConverterMappingInfo"/>.
        /// </summary>
        public static IEnumerable<IConverterMappingInfo> ConverterMappings => ValueConverterRegister.ConverterMappings;

        /// <summary>
        /// Gets an instance of the <see cref="CatalystDbContext"/>.
        /// </summary>
        internal static ICatalystDbContext DbContext => Container.GetInstance<CatalystDbContext>(Constants.Database.ConnectionStringName);

        /// <summary>
        /// Gets the <see cref="IValueConverterRegister"/>.
        /// </summary>
        internal static IValueConverterRegister ValueConverterRegister => Container.GetInstance<IValueConverterRegister>();

        /// <summary>
        /// Gets or sets the <see cref="IServiceContainer"/>.
        /// </summary>
        /// <exception cref="NullReferenceException">
        /// Throws an exception if the singleton has not been instantiated.
        /// </exception>
        internal static IServiceContainer Container
        {
            get
            {
                if (_container == null) throw new NullReferenceException("Container has not been set");
                return _container;
            }

            set
            {
                _container = value;
            }
        }
    }
}