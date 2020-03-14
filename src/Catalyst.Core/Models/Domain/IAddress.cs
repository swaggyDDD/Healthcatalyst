namespace Catalyst.Core.Models.Domain
{
    /// <summary>
    /// Represents an address.
    /// </summary>
    public interface IAddress
    {
        /// <summary>
        /// Gets or sets a descriptive name or alias for the address.
        /// </summary>
        /// <remarks>
        /// Useful when presenting collections of addresses
        /// </remarks>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the first address line
        /// </summary>
        string Address1 { get; set; }

        /// <summary>
        /// Gets or sets the second address line
        /// </summary>
        string Address2 { get; set; }

        /// <summary>
        /// Gets or sets the city or locality of the address
        /// </summary>
        string Locality { get; set; }

        /// <summary>
        /// Gets or sets the state or province of the address
        /// </summary>
        string Region { get; set; }

        /// <summary>
        /// Gets or sets the postal code of the address
        /// </summary>
        string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the country code of the address
        /// </summary>
        string CountryCode { get; set; }
    }
}