namespace Catalyst.Core.ValueConverters
{
    using System;
    using System.Reflection;

    using Catalyst.Core.Models.Domain;
    using Catalyst.Core.Models.PropData;

    using Newtonsoft.Json;

    /// <inheritdoc />
    public abstract class PropertyValueConverterBase<TValue> : IPropertyValueConverter
        where TValue : class, IPropertyValueModel, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyValueConverterBase{TValue}"/> class.
        /// </summary>
        /// <param name="prop">
        /// The prop.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Throws an exception if the property is null
        /// </exception>
        protected PropertyValueConverterBase(ExtendedProperty prop)
        {
            if (prop == null) throw new ArgumentNullException(nameof(prop));
            Property = prop;
        }

        /// <summary>
        /// Gets the property.
        /// </summary>
        public ExtendedProperty Property { get; }

        /// <inheritdoc />
        public Type ModelType => typeof(TValue);

        /// <summary>
        /// Gets the sort order.
        /// </summary>
        public int SortOrder
        {
            get
            {
                var att = this.GetType().GetCustomAttribute<ConverterAliasAttribute>(false);
                return att?.SortOrder ?? 1000;
            }
        }

        /// <inheritdoc />
        public virtual void SetValue(object value)
        {
            if (value == null) value = Activator.CreateInstance(ModelType);
            Property.Value = JsonConvert.SerializeObject(value);
        }

        /// <inheritdoc />
        public virtual T GetPropertyValue<T>() where T : class, IPropertyValueModel, new()
        {
            return GetValue() as T;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public object GetValue()
        {
            return !this.Property.Value.IsNullOrWhiteSpace()
                       ? JsonConvert.DeserializeObject(Property.Value, ModelType)
                       : Activator.CreateInstance(ModelType);
        }
    }
}