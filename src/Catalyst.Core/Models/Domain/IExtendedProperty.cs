namespace Catalyst.Core.Models.Domain
{
    /// <summary>
    /// Represents an extended property.
    /// </summary>
    public interface IExtendedProperty : IEntity
    {
        /// <summary>
        /// Gets or sets the alias for the value converter.
        /// </summary>
        string ConverterAlias { get; set; }

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        string Value { get; set; }
    }
}