namespace Catalyst.Core.ValueConverters
{
    using System;

    using Catalyst.Core.Models.Domain;
    using Catalyst.Core.Models.PropData;

 
    /// <summary>
    /// Represents a property value converter.
    /// </summary>
    public interface IPropertyValueConverter
    {
        /// <summary>
        /// Gets the property.
        /// </summary>
        ExtendedProperty Property { get; }

        /// <summary>
        /// Gets the model type.
        /// </summary>
        Type ModelType { get; }

        /// <summary>
        /// Gets the sort order.
        /// </summary>
        int SortOrder { get; }

        /// <summary>
        /// Gets the typed property value.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the model
        /// </typeparam>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        T GetPropertyValue<T>() where T : class, IPropertyValueModel, new();

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        object GetValue();

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        void SetValue(object value);
    }
}