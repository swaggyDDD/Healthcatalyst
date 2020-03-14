namespace Catalyst.Core.Models
{
    /// <summary>
    /// Represents a country.
    /// </summary>
    public interface ICountry
    {
        /// <summary>
        /// Gets or sets the country code.
        /// </summary>
        string Code { get; set; }

        /// <summary>
        /// Gets or sets the country name.
        /// </summary>
        string Name { get; set; }
    }
}