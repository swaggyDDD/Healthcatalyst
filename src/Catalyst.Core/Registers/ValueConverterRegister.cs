namespace Catalyst.Core.Registers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Catalyst.Core.Logging;
    using Catalyst.Core.Models.Domain;
    using Catalyst.Core.ValueConverters;

    using LightInject;

    using Merchello.Core;

    /// <inheritdoc />
    internal class ValueConverterRegister : IValueConverterRegister
    {
        /// <summary>
        /// The <see cref="Type"/>s registered.
        /// </summary>
        private readonly HashSet<Type> _types = new HashSet<Type>();

        /// <summary>
        /// The <see cref="ILogger"/>.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// The <see cref="IServiceContainer"/>.
        /// </summary>
        private readonly IServiceContainer _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueConverterRegister"/> class.
        /// </summary>
        /// <param name="container">The <see cref="IServiceContainer"/></param>
        /// <param name="logger">The logger.</param>
        public ValueConverterRegister(IServiceContainer container, ILogger logger)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (logger == null) throw new ArgumentNullException(nameof(logger));

            this._container = container;
            this._logger = logger;
            this.Initialize();
        }

        /// <inheritdoc />
        public IEnumerable<IConverterMappingInfo> ConverterMappings
        {
            get
            {
                return
                    InstanceTypes
                    .Select(x => x.GetCustomAttribute<ConverterAliasAttribute>(false))
                        .OrderBy(x => x.SortOrder);
            }
        }

        /// <inheritdoc />
        public IEnumerable<Type> InstanceTypes => _types;

        /// <inheritdoc />
        public IPropertyValueConverter GetInstanceFor(IExtendedProperty property)
        {
            return _container.GetInstance<IExtendedProperty, IPropertyValueConverter>(property, property.ConverterAlias);
        }

        /// <summary>
        /// Initializes the registry.
        /// </summary>
        private void Initialize()
        {
            this._logger.Info<ValueConverterRegister>("Initializing");

            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
            .Where(type => !string.IsNullOrEmpty(type.Namespace))
            .Where(type => type.BaseType != null && type.BaseType.IsGenericType
            && type.BaseType.GetGenericTypeDefinition() == typeof(PropertyValueConverterBase<>)).ToArray();


            this._logger.Info<DbMappingRegister>($"Found {typesToRegister.Count()} types to register");
            
            foreach (var t in typesToRegister)
            {
                var att = t.GetCustomAttribute<ConverterAliasAttribute>(false);
                if (att != null)
                {
                    this._logger.Info<DbMappingRegister>($"Adding {t.FullName} to registry");
                    this._types.Add(t);

                    // register the type with the container - named with the CoverterAlias
                    _container.Register<IExtendedProperty, IPropertyValueConverter>(
                        (factory, prop) =>
                            {
                                var activator = factory.GetInstance<IActivatorServiceProvider>();
                                var converter = activator.GetService<IPropertyValueConverter>(
                                    t, 
                                    new object[] { prop });

                                return converter;
                            }, 
                        att.ConverterAlias);

                }
            }

            this._logger.Info<ValueConverterRegister>("Completed adding types to register");
        }
    }
}