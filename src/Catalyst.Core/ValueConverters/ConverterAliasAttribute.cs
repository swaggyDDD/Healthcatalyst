namespace Catalyst.Core.ValueConverters
{
    using System;

    using Catalyst.Core.Models.Domain;

    /// <summary>
    /// Represents an attribute that associates property value converters with <see cref="IExtendedProperty"/> values.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ConverterAliasAttribute : Attribute, IConverterMappingInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConverterAliasAttribute"/> class.
        /// </summary>
        /// <param name="alias">
        /// The alias.
        /// </param>
        /// <param name="valueType">
        /// The value Type.
        /// </param>
        /// <param name="sortOrder">
        /// Sort order.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Alias is required
        /// </exception>
        public ConverterAliasAttribute(string alias, Type valueType, int sortOrder)
        {
            if (alias.IsNullOrWhiteSpace()) throw new ArgumentNullException(nameof(alias));
            if (valueType == null) throw new ArgumentNullException(nameof(valueType));

            ConverterAlias = alias;
            ValueType = valueType;
            SortOrder = sortOrder;
        }

        /// <inheritdoc />
        public string ConverterAlias { get; }

        /// <inheritdoc />
        public Type ValueType { get; }

        /// <inheritdoc />
        public int SortOrder { get; set; }
    }
}