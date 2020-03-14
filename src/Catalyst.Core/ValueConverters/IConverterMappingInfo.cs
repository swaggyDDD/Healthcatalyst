namespace Catalyst.Core.ValueConverters
{
    using System;

    using Catalyst.Core.Models.Domain;

    /// <summary>
    /// Represents converter mapping information.
    /// </summary>
    public interface IConverterMappingInfo
    {
        /// <summary>
        /// Gets the converter alias.
        /// </summary>
        /// <remarks>
        /// This MUST match the "ConverterAlias" property associated with the <see cref="IExtendedProperty"/> value
        /// </remarks>
        string ConverterAlias { get; }

        /// <summary>
        /// Gets the value type handled by the converter.
        /// </summary>
        Type ValueType { get; }

        /// <summary>
        /// Gets the sort order.
        /// </summary>
        int SortOrder { get; }
    }
}