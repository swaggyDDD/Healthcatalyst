namespace Catalyst.Core.Registers
{
    using System;
    using System.Collections.Generic;

    using Catalyst.Core.Models.Domain;
    using Catalyst.Core.ValueConverters;

    /// <summary>
    /// Represents the value converter register.
    /// </summary>
    internal interface IValueConverterRegister : ITypeRegister
    {
        /// <summary>
        /// Gets the collection of "distinct known" converter aliases.
        /// </summary>
        IEnumerable<IConverterMappingInfo> ConverterMappings { get; }


        /// <summary>
        /// Gets the property value converter for a given property.
        /// </summary>
        /// <param name="property">
        /// The property to be converted.
        /// </param>
        /// <returns>
        /// The <see cref="IPropertyValueConverter"/>.
        /// </returns>
        IPropertyValueConverter GetInstanceFor(IExtendedProperty property);
    }
}